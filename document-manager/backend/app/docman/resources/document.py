import magic
import uuid
import os
from datetime import datetime

from werkzeug.exceptions import BadRequest, NotFound, Conflict, RequestEntityTooLarge, InternalServerError
from flask import request, current_app, send_file, make_response, jsonify
from flask_restx import Resource, reqparse

from app.docman.models.document import Document
from app.extensions import api, cache, jwt
from app.constants import FILE_UPLOAD_SIZE, FILE_UPLOAD_OFFSET, FILE_UPLOAD_PATH, DOWNLOAD_TOKEN, TIMEOUT_5_MINUTES, TIMEOUT_24_HOURS, TUS_API_VERSION, TUS_API_SUPPORTED_VERSIONS, FORBIDDEN_FILETYPES


def validate_filename(filename):
    if not filename:
        raise BadRequest('File name is required')
    if filename.endswith(FORBIDDEN_FILETYPES):
        raise BadRequest('File type is forbidden')

    return filename


def to_absolute_folder(folder):
    if not folder:
        raise BadRequest('Folder is required')

    base_folder = current_app.config['UPLOADED_DOCUMENT_DEST']
    combined_folder = os.path.join(base_folder, folder)
    # Ensure that the upload folder lies under the base directory (and so has not been manipulated in a manner such as 'app/document_uploads/../../etc')
    if not os.path.abspath(combined_folder).startswith(base_folder):
        raise BadRequest('Supplied folder path is invalid')

    return combined_folder


def validate_file_size(file_size):
    if not file_size:
        raise BadRequest('Received file upload of unspecified size')

    size = int(file_size)
    if size <= 0:
        raise BadRequest('File size must be a positve number')

    max_file_size = current_app.config['MAX_CONTENT_LENGTH']
    if size > max_file_size:
        raise RequestEntityTooLarge(f'The maximum file upload size is {max_file_size/1024/1024}MB.')

    return size


@api.route('/documents/uploads')
class DocumentUploadResource(Resource):
    @jwt.requires_auth
    def post(self):
        if request.headers.get('Tus-Resumable') is None:
            raise BadRequest('Received file upload for unsupported file transfer protocol')

        parser = reqparse.RequestParser(trim=True)
        parser.add_argument('filename', type=str, required=True, help='File name + extension of the document.')

        filename = validate_filename(parser.parse_args().get('filename'))
        file_size = validate_file_size(request.headers.get('Upload-Length'))

        document_guid = str(uuid.uuid4())
        destination_folder = to_absolute_folder("temp")
        file_path = os.path.join(destination_folder, document_guid)

        try:
            if not os.path.exists(destination_folder):
                os.makedirs(destination_folder)
            with open(file_path, 'wb') as f:
                f.seek(file_size - 1)
                f.write(b"\0")
        except IOError as e:
            raise InternalServerError('Unable to create file')

        cache.set(FILE_UPLOAD_SIZE(document_guid), file_size, TIMEOUT_24_HOURS)
        cache.set(FILE_UPLOAD_OFFSET(document_guid), 0, TIMEOUT_24_HOURS)
        cache.set(FILE_UPLOAD_PATH(document_guid), file_path, TIMEOUT_24_HOURS)

        document = Document(document_guid=document_guid,
                            full_storage_path=file_path,
                            upload_started_date=datetime.utcnow(),
                            filename=filename,
                            submitted=False)
        document.save()

        response = make_response(jsonify(document_guid=document_guid), 201)
        response.headers['Tus-Resumable'] = TUS_API_VERSION
        response.headers['Tus-Version'] = TUS_API_SUPPORTED_VERSIONS
        response.headers['Location'] = os.path.join(current_app.config['DOCUMENT_MANAGER_URL'], 'documents', 'uploads', document_guid)
        response.headers['Upload-Offset'] = 0
        response.headers['Access-Control-Expose-Headers'] = "Tus-Resumable,Tus-Version,Location,Upload-Offset"
        response.autocorrect_location_header = False
        return response

    # Used to delete a file upload that is still in temporary storage.
    # Expects the document GUID in a plaintext body
    def delete(self):
        # Should check request size so a large body wont be processed
        if request.content_length > 350:
            raise BadRequest('Request body too large')

        document_guid = request.get_data(as_text=True)
        if not document_guid:
            raise BadRequest('Must supply a document GUID')

        doc = Document.find_by_document_guid(document_guid)
        if not doc or doc.submitted:
            raise NotFound(f'Upload not found with GUID {document_guid}')

        try:
            os.remove(doc.full_storage_path)
        except IOError as e:
            raise InternalServerError('Unable to delete file')

        doc.delete()
        cache.delete(FILE_UPLOAD_SIZE(document_guid))
        cache.delete(FILE_UPLOAD_OFFSET(document_guid))
        cache.delete(FILE_UPLOAD_PATH(document_guid))

    # Used to fetch/load file metadata from the frontend.
    # The 'restore' query string is marked as required because it is the only action we support currently.
    def get(self):
        parser = reqparse.RequestParser(trim=True)
        parser.add_argument('restore', type=str, location='args', required=True)

        document_guid = parser.parse_args().get('restore')
        doc = Document.find_by_document_guid(document_guid)
        if not doc or doc.submitted:
            raise NotFound(f'Upload not found with GUID {document_guid}')

        response = make_response("", 200)
        response.headers['Access-Control-Expose-Headers'] = 'Content-Disposition, Content-Length, X-Content-Transfer-Id'
        response.headers['Content-Disposition'] = f'inline; filename="{doc.filename}"'
        response.headers['Content-Length'] = os.stat(doc.full_storage_path).st_size
        response.headers['Content-Type'] = magic.from_file(doc.full_storage_path, mime=True)
        response.headers['X-Content-Transfer-Id'] = document_guid
        return response


@api.route('/documents/uploads/<string:document_guid>')
class DocumentUploadManagementResource(Resource):
    def patch(self, document_guid):
        file_path = cache.get(FILE_UPLOAD_PATH(document_guid))
        if file_path is None or not os.path.lexists(file_path):
            raise NotFound('PATCH sent for an upload that does not exist')

        request_offset = int(request.headers.get('Upload-Offset', 0))
        file_offset = cache.get(FILE_UPLOAD_OFFSET(document_guid))
        if request_offset != file_offset:
            raise Conflict("Offset in request does not match uploaded file's offset")

        chunk_size = request.headers.get('Content-Length')
        if chunk_size is None:
            raise BadRequest('No Content-Length header in request')
        chunk_size = int(chunk_size)
        if chunk_size <= 0:
            raise BadRequest('Content-Length header must be a positive value')

        new_offset = file_offset + chunk_size
        file_size = cache.get(FILE_UPLOAD_SIZE(document_guid))
        if new_offset > file_size:
            raise RequestEntityTooLarge('The uploaded chunk would put the file above its declared file size.')

        try:
            with open(file_path, "r+b") as f:
                f.seek(file_offset)
                f.write(request.data)
        except IOError as e:
            raise InternalServerError('Unable to write to file')

        if new_offset == file_size:
            # File transfer complete.
            doc = Document.find_by_document_guid(document_guid)
            doc.upload_completed_date = datetime.utcnow()
            doc.save()

            cache.delete(FILE_UPLOAD_SIZE(document_guid))
            cache.delete(FILE_UPLOAD_OFFSET(document_guid))
            cache.delete(FILE_UPLOAD_PATH(document_guid))
        else:
            # File upload still in progress
            cache.set(FILE_UPLOAD_OFFSET(document_guid), new_offset, TIMEOUT_24_HOURS)

        response = make_response('', 204)
        response.headers['Tus-Resumable'] = TUS_API_VERSION
        response.headers['Tus-Version'] = TUS_API_SUPPORTED_VERSIONS
        response.headers['Upload-Offset'] = new_offset
        response.headers['Access-Control-Expose-Headers'] = 'Tus-Resumable,Tus-Version,Upload-Offset'
        return response

    def head(self, document_guid):
        if document_guid is None:
            raise BadRequest('Must specify document GUID in HEAD')

        file_path = cache.get(FILE_UPLOAD_PATH(document_guid))
        if file_path is None or not os.path.lexists(file_path):
            raise NotFound('File does not exist')

        response = make_response("", 200)
        response.headers['Tus-Resumable'] = TUS_API_VERSION
        response.headers['Tus-Version'] = TUS_API_SUPPORTED_VERSIONS
        response.headers['Upload-Offset'] = cache.get(FILE_UPLOAD_OFFSET(document_guid))
        response.headers['Upload-Length'] = cache.get(FILE_UPLOAD_SIZE(document_guid))
        response.headers['Cache-Control'] = 'no-store'
        response.headers['Access-Control-Expose-Headers'] = 'Tus-Resumable,Tus-Version,Upload-Offset,Upload-Length,Cache-Control'
        return response

    def options(self, document_guid):
        response = make_response('', 200)

        # if request.headers.get('Access-Control-Request-Method', None) is not None:
        #     # CORS request, return 200
        #     return response

        response.headers['Tus-Resumable'] = TUS_API_VERSION
        response.headers['Tus-Version'] = TUS_API_SUPPORTED_VERSIONS
        response.headers['Tus-Extension'] = "creation"
        response.headers['Tus-Max-Size'] = current_app.config['MAX_CONTENT_LENGTH']
        response.headers['Access-Control-Expose-Headers'] = 'Tus-Resumable,Tus-Version,Tus-Extension,Tus-Max-Size'
        response.status_code = 204
        return response


@api.route('/documents/uploads/<string:document_guid>/submit')
class DocumentUploadSubmissionResource(Resource):
    # Finalizes a file upload, making it no longer directly accessable from the frontend
    @jwt.requires_auth
    def post(self, document_guid):
        parser = reqparse.RequestParser(trim=True)
        parser.add_argument('folder', type=str, required=True, help='The sub folder path to store the document in.')
        folder = parser.parse_args().get('folder')

        doc = Document.find_by_document_guid(document_guid)
        if not doc:
            raise NotFound(f'Upload not found with GUID {document_guid}')
        if doc.submitted:
            raise Conflict('Upload is already submitted')

        destination_folder = to_absolute_folder(folder)
        destination_path = os.path.join(destination_folder, document_guid)
        if os.path.isfile(destination_path):
            raise InternalServerError('File already exists at destination')

        try:
            if not os.path.exists(destination_folder):
                os.makedirs(destination_folder)
            os.rename(doc.full_storage_path, destination_path)
        except IOError as e:
            raise InternalServerError('Unable to write to file')

        doc.full_storage_path = destination_path
        doc.submitted = True
        doc.save()

        return make_response(doc.filename, 200)


@api.route('/documents')
class DocumentListResource(Resource):
    # Direct file upload from PRIME API
    @jwt.requires_auth
    def post(self):
        parser = reqparse.RequestParser(trim=True)
        parser.add_argument('folder', type=str, location='args', required=True, help='The sub folder path to store the document in.')
        parser.add_argument('filename', type=str, location='args', required=True, help='File name + extension of the document.')
        data = parser.parse_args()

        filename = validate_filename(data.get('filename'))
        destination_folder = to_absolute_folder(data.get('folder'))
        validate_file_size(request.content_length)

        document_guid = str(uuid.uuid4())
        file_path = os.path.join(destination_folder, document_guid)

        try:
            if not os.path.exists(destination_folder):
                os.makedirs(destination_folder)
            with open(file_path, 'wb') as f:
                f.write(request.data)
        except IOError as e:
            raise InternalServerError('Unable to write to file')

        document = Document(
            document_guid=document_guid,
            full_storage_path=file_path,
            upload_started_date=datetime.utcnow(),
            upload_completed_date=datetime.utcnow(),
            filename=filename,
            submitted=True
        )
        document.save()

        return make_response(jsonify(document_guid=document_guid), 201)


@api.route('/documents/<string:document_guid>')
class DocumentResource(Resource):
    @jwt.requires_auth
    def get(self, document_guid):
        if not document_guid:
            raise BadRequest('Document GUID is required')

        doc = Document.find_by_document_guid(document_guid)
        if not doc:
            raise NotFound()

        return send_file(path_or_file=doc.full_storage_path,
                         download_name=doc.filename,
                         as_attachment=True)


@api.route('/documents/<string:document_guid>/download-token')
class DownloadTokenCreationResource(Resource):
    @jwt.requires_auth
    def post(self, document_guid):
        if not document_guid:
            raise BadRequest('Must specify document GUID')

        doc = Document.find_by_document_guid(document_guid)
        if not doc:
            raise NotFound('Could not find document')

        if not doc.upload_completed_date:
            raise BadRequest('File upload not complete')

        token = str(uuid.uuid4())
        cache.set(DOWNLOAD_TOKEN(token), document_guid, TIMEOUT_5_MINUTES)

        return {'token': token}


@api.route('/documents/downloads/<string:token>')
class DocumentDownloadResource(Resource):
    def get(self, token):
        if not token:
            raise BadRequest('Must specify token')

        doc_guid = cache.get(DOWNLOAD_TOKEN(token))
        cache.delete(DOWNLOAD_TOKEN(token))

        if not doc_guid:
            raise NotFound('Could not find token')

        doc = Document.find_by_document_guid(doc_guid)
        if not doc:
            raise NotFound('Could not find document')

        return send_file(path_or_file=doc.full_storage_path,
                         download_name=doc.filename,
                         as_attachment=True)
