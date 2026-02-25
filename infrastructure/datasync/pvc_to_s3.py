# -------------------------------------------------------------------------------
# Name:        objstor_datasync.py
# Purpose:     sync files between S3 bucket and OpenShift pvc
#
# Author:      Roya Shourouni
#
# Created:     2026-02-01
# Notes: This is a little-tested proof of concept upload/download functionality
# -------------------------------------------------------------------------------
import os
import subprocess
import sys
import objstor_constants

from minio import Minio
from minio.error import S3Error
from kubernetes import client, config



# copy a pvc file to bucket
def connect_to_s3():
    return Minio(
        endpoint=objstor_constants.OBJSTOR_ENDPOINT,
        access_key=objstor_constants.ACCESS_KEY,
        secret_key=objstor_constants.SECRET_KEY,
        region="US",
    )
    
def stream_pg_dump_to_s3():
    # Command to run inside the pod
    try:

        # Command to run inside the pod
        # -Fc for custom format, or remove for plain SQL
        pg_dump_cmd = [
            "oc", "exec", "-n", objstor_constants.NAMESPACE, objstor_constants.POD_NAME, "--",
            "pg_dump", "-U", objstor_constants.DB_USER, "-d", objstor_constants.DB_NAME, '--table=public."PharmanetTransactionLog"',
            "--data-only", "--column-inserts", "--where=\"CreatedTimeStamp\" >= '2023-01-01 00:00:00' AND \"CreatedTimeStamp\" < '2024-01-01 00:00:00'", "-Fc"
        ]
            
        minio_client =connect_to_s3()
        with subprocess.Popen(pg_dump_cmd, stdout=subprocess.PIPE, env=os.environ.copy()) as proc:
            # upload file, never been run
            minio_client.put_object(
            bucket_name=objstor_constants.S3_BUCKET_NAME, object_name="prime_prod.dump",data= proc.stdout , length=-1, part_size=5000*1024*1024 )


    except Exception as e:
        print(f"❌ Error: {e}")
        sys.exit(1)

def main(argv):
    # Load kubeconfig (works for OpenShift too)
    config.load_kube_config()  # For local dev

    # Kubernetes API client
    core_v1 = client.CoreV1Api()

    # put pvc files into bucket
    stream_pg_dump_to_s3()



if __name__ == "__main__":
    try:
        main(sys.argv[1:])
    except S3Error as exc:
        print("error occurred.", exc)