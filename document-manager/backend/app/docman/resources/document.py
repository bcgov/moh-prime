import uuid
import os
from datetime import datetime

from werkzeug.exceptions import BadRequest, NotFound, Conflict, RequestEntityTooLarge, InternalServerError
from flask import request, current_app, send_file, make_response, jsonify
from flask_restplus import Resource, reqparse

from app.docman.models.document import Document
from app.extensions import api, cache, jwt
from app.constants import FILE_UPLOAD_SIZE, FILE_UPLOAD_OFFSET, FILE_UPLOAD_PATH, DOWNLOAD_TOKEN, TIMEOUT_5_MINUTES, TIMEOUT_24_HOURS, TUS_API_VERSION, TUS_API_SUPPORTED_VERSIONS, FORBIDDEN_FILETYPES


@api.route('/documents')
class DocumentListResource(Resource):
    parser = reqparse.RequestParser(trim=True)
    parser.add_argument(
        'folder', type=str, required=False, help='The sub folder path to store the document in.')
    parser.add_argument(
        'filename', type=str, required=False, help='File name + extension of the document.')

    @jwt.requires_auth
    def post(self):
        if request.headers.get('Tus-Resumable') is None:
            raise BadRequest('Received file upload for unsupported file transfer protocol')

        file_size = request.headers.get('Upload-Length')
        if not file_size:
            raise BadRequest('Received file upload of unspecified size')
        file_size = int(file_size)

        max_file_size = current_app.config['MAX_CONTENT_LENGTH']
        if file_size > max_file_size:
            raise RequestEntityTooLarge(f'The maximum file upload size is {max_file_size/1024/1024}MB.')

        data = self.parser.parse_args()
        filename = data.get('filename')
        if not filename:
            raise BadRequest('File name is required')
        if filename.endswith(FORBIDDEN_FILETYPES):
            raise BadRequest('File type is forbidden')

        base_folder = current_app.config['UPLOADED_DOCUMENT_DEST']
        folder = data.get('folder')
        folder = os.path.join(base_folder, folder)
        if not self.is_safe_path(base_folder, folder):
          raise BadRequest('Supplied folder path is invalid')

        document_guid = str(uuid.uuid4())
        file_path = os.path.join(folder, document_guid)

        try:
            if not os.path.exists(folder):
                os.makedirs(folder)
            with open(file_path, 'wb') as f:
                f.seek(file_size - 1)
                f.write(b"\0")
        except IOError as e:
            raise InternalServerError('Unable to create file')

        cache.set(FILE_UPLOAD_SIZE(document_guid), file_size, TIMEOUT_24_HOURS)
        cache.set(FILE_UPLOAD_OFFSET(document_guid), 0, TIMEOUT_24_HOURS)
        cache.set(FILE_UPLOAD_PATH(document_guid), file_path, TIMEOUT_24_HOURS)

        document = Document(
            document_guid=document_guid,
            full_storage_path=file_path,
            upload_started_date=datetime.utcnow(),
            filename=filename,
        )
        document.save()

        response = make_response(jsonify(document_guid=document_guid), 201)
        response.headers['Tus-Resumable'] = TUS_API_VERSION
        response.headers['Tus-Version'] = TUS_API_SUPPORTED_VERSIONS
        response.headers['Location'] = f'{current_app.config["DOCUMENT_MANAGER_URL"]}/documents/{document_guid}'
        response.headers['Upload-Offset'] = 0
        response.headers['Access-Control-Expose-Headers'] = "Tus-Resumable,Tus-Version,Location,Upload-Offset"
        response.autocorrect_location_header = False
        return response

    # Ensure that a given path lies under a given base directory (and so has not been manipulated in a manner such as 'app/document_uploads/../../etc')
    def is_safe_path(self, basedir, path):
      return os.path.abspath(path).startswith(basedir)


@api.route(f'/documents/<string:document_guid>')
class DocumentResource(Resource):
    @jwt.requires_auth
    def get(self, document_guid):
        if not document_guid:
            raise BadRequest('Document GUID is required')

        doc = Document.find_by_document_guid(document_guid)
        if not doc:
            raise NotFound()

        return send_file(
            filename_or_fp=doc.full_storage_path,
            attachment_filename=doc.filename,
            as_attachment=False)

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
        response.headers['Access-Control-Expose-Headers'] = "Tus-Resumable,Tus-Version,Upload-Offset"
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
        response.headers['Access-Control-Expose-Headers'] = "Tus-Resumable,Tus-Version,Upload-Offset,Upload-Length,Cache-Control"
        return response

    def options(self, document_guid):
        response = make_response('', 200)

        # if request.headers.get('Access-Control-Request-Method', None) is not None:
        #     # CORS request, return 200
        #     return response

        response.headers['Tus-Resumable'] = TUS_API_VERSION
        response.headers['Tus-Version'] = TUS_API_SUPPORTED_VERSIONS
        response.headers['Tus-Extension'] = "creation"
        response.headers['Tus-Max-Size'] = current_app.config["MAX_FILE_SIZE"]
        response.headers['Access-Control-Expose-Headers'] = "Tus-Resumable,Tus-Version,Tus-Extension,Tus-Max-Size"
        response.status_code = 204
        return response


@api.route(f'/documents/<string:document_guid>/download-token')
class DownloadTokenCreationResource(Resource):
    @jwt.requires_auth
    def post(self, document_guid):
        if not document_guid:
            raise BadRequest('Must specify document GUID')

        token = str(uuid.uuid4())
        cache.set(DOWNLOAD_TOKEN(token), document_guid, TIMEOUT_5_MINUTES)

        return {'token': token}


@api.route(f'/documents/downloads/<string:token>')
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

        return send_file(
            filename_or_fp=doc.full_storage_path,
            attachment_filename=doc.filename,
            as_attachment=True)
