# -------------------------------------------------------------------------------
# Name:        objstor_datasync.py
# Purpose:     sync files between S3 bucket and OpenShift pvc
#
# Author:      Roya Shourouni
#
# Created:     2026-02-01
# Notes: This is a little-tested proof of concept upload/download functionality
# -------------------------------------------------------------------------------

import datetime as dt
import os
import re
import smtplib
import socket
import sys
import objstor_constants

from datetime import datetime
from email.mime.multipart import MIMEMultipart
from email.mime.text import MIMEText
from minio import Minio
from minio.error import S3Error

# update to be the directory in the pod that the PVC is mounted to
pvc_directory = "/home/postgres/pgdata/pgroot/data"
# pvc_directory = "J:\\Scripts\\Testing\\Sync_directory"


# copy a pvc file to bucket
def copy_to_bucket(minio_client, pvc_directory, file_name):
    print("copying to bucket: ", file_name)

    # upload file, never been run
    minio_client.fput_object(
        objstor_constants.S3_BUCKET_NAME,
        file_name,
        os.path.join(pvc_directory, file_name.lower()),
    )
    return


# copy a bucket file to pvc
def copy_to_pvc(minio_client, file_name, last_modified, pvc_directory):
    print("copying to pvc: ", file_name, pvc_directory)
    minio_client.fget_object(
        objstor_constants.S3_BUCKET_NAME,
        file_name,
        os.path.join(pvc_directory, file_name.lower()),
    )
    os.utime(os.path.join(pvc_directory, file_name.lower()), (last_modified, last_modified))


def main(argv):

    # objstor_constants.print_constants()

    minio_client = Minio(
        endpoint=objstor_constants.OBJSTOR_ENDPOINT,
        access_key=objstor_constants.ACCESS_KEY,
        secret_key=objstor_constants.SECRET_KEY,
        region="US",
    )

    # create a comparison dictionary
    file_dict = {}

    # add bucket file names and last modified timestamp to comparison dictionary
    bucket_files = minio_client.list_objects(
        objstor_constants.S3_BUCKET_NAME,
        recursive=True,
        use_url_encoding_type=False,
    )
    for bucket_file in bucket_files:
        file_name = bucket_file.object_name
        # Debugging purposes
        # print(f"file_name: {file_name}")
        if bucket_file.last_modified is None:
            print(f"file {bucket_file} missing last_modified")
            continue
        file_date = datetime.timestamp(bucket_file.last_modified)
        if not bucket_file.is_dir:
            file_dict[file_name] = {
                "file_name": file_name,
                "bucket_last_modified": file_date,
            }

    # add pvc file names and last modified timestamp to comparison dictionary
    for dirname, dirnames, filenames in os.walk(pvc_directory):

        # print path to all filenames.
        for file_name in filenames:

            rel_dir = os.path.relpath(dirname, pvc_directory)
            rel_file = file_name
            if rel_dir != ".":
                rel_file = os.path.join(rel_dir, file_name)
                # next line only needed for Windows as it uses backslashes instead of forward slashes
                if os.name == "nt":
                    rel_file = rel_file.replace("\\", "/")
            file_date = os.path.getmtime(os.path.join(dirname, file_name))
            if rel_file in file_dict:
                file_dict[rel_file]["pvc_last_modified"] = file_date
            else:
                file_dict[rel_file] = {
                    "file_name": rel_file,
                    "pvc_last_modified": file_date,
                }

    # put newer bucket files into pvc, and newer pvc files into bucket, adjust timestamps
    pvc_timestamp_sync_list = []
    upper_file_names = []
    upper_pattern = re.compile(r".*[A-Z].*")
    for file_name in file_dict:
        if upper_pattern.fullmatch(file_name):
            # if the file name or path has an uppercase character, track and skip it
            upper_file_names.append(file_name)
            continue
        file = file_dict[file_name]
        if "pvc_last_modified" in file and "bucket_last_modified" in file:
            # both directories have a copy of the file
            if file["pvc_last_modified"] > file["bucket_last_modified"]:
                # pvc has newer file
                copy_to_bucket(minio_client, pvc_directory, file_name)
                pvc_timestamp_sync_list.append(file_name)
            elif file["pvc_last_modified"] < file["bucket_last_modified"]:
                # bucket has newer file
                copy_to_pvc(
                    minio_client, file_name, file["bucket_last_modified"], pvc_directory
                )
            # no work to do if the same last modified date

        elif "pvc_last_modified" in file:
            # file is only in the pvc
            copy_to_bucket(minio_client, pvc_directory, file_name)
            pvc_timestamp_sync_list.append(file_name)

        else:
            # file is only in the bucket
            copy_to_pvc(
                minio_client, file_name, file["bucket_last_modified"], pvc_directory
            )

    # sync the pvc timestamps up with the new bucket files, as we can't update timestamps on bucket files
    print("Syncing pvc modified timestamps with object storage")
    bucket_files = minio_client.list_objects(
        objstor_constants.S3_BUCKET_NAME,
        recursive=True,
        use_url_encoding_type=False,
    )
    for bucket_file in bucket_files:
        # Debugging purposes
        # print(f"file_name: {bucket_file}")
        if bucket_file.last_modified is None:
            print(f"file {bucket_file} missing last_modified")
            continue
        file_name = bucket_file.object_name
        bucket_last_modified = datetime.timestamp(bucket_file.last_modified)
        if file_name in pvc_timestamp_sync_list:
            os.utime(
                os.path.join(pvc_directory, file_name),
                (bucket_last_modified, bucket_last_modified),
            )

    if len(upper_file_names) > 0 and dt.datetime.now().hour in range(20, 23):
        # Send email to admin notifying of any uppercase file names in the PVC that weren't sync'd
        # The script runs every 3h, Only send the email once in the evening
        msg = MIMEMultipart("related")
        msg["Subject"] = "Script Report"
        msg["To"] = objstor_constants.ADMIN_EMAIL
        msg["From"] = objstor_constants.ADMIN_EMAIL

        host_name = socket.gethostname()
        dir_path = os.path.dirname(os.path.realpath(__file__))
        file_names = ""
        for upper_file in upper_file_names:
            file_names += upper_file + "<br>"
        html = "<html><head></head><body><p>" \
            + "A scheduled script wiof_objstor_datasync.py has sent an automated report email." \
            + "<br />Server: " + str(host_name) \
            + "<br />File Path: " + dir_path + "<br />" \
            + "The following files in the PVC are uppercase, so were not synchronized with object storage:" \
            + file_names \
            + "</p></body></html>"
        msg.attach(MIMEText(html, "html"))
        s = smtplib.SMTP(objstor_constants.SMTP_SERVER)
        s.sendmail(msg["From"], msg["To"], msg.as_string())
        s.quit()


if __name__ == "__main__":
    try:
        print("Starting copy...")
        main(sys.argv[1:])
        print("Copy finished.")
    except S3Error as exc:
        print("error occurred.", exc)