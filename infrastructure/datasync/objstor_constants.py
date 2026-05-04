import os
import dotenv
import logging

envPath = os.path.join(os.path.dirname(__file__), '.env')
if os.path.exists(envPath):
    logging.info("loading dot env...")
    dotenv.load_dotenv()

# S3 Compatible Object Storage configuration
ACCESS_KEY = os.environ["ACCESS_KEY"]
SECRET_KEY = os.environ["SECRET_KEY"]
S3_BUCKET_NAME = os.environ["S3_BUCKET_NAME"]
OBJSTOR_ENDPOINT = os.environ["OBJSTOR_ENDPOINT"]

# Openshift configuration
NAMESPACE = os.environ["NAMESPACE"]
POD_NAME = os.environ["POD_NAME"]
DB_NAME = os.environ["DB_NAME"]
DB_USER = os.environ["DB_USER"]
PG_PASSWORD = os.environ["PG_PASSWORD"]


ADMIN_EMAIL = os.environ["ADMIN_EMAIL"]
SMTP_SERVER = os.environ["SMTP_SERVER"]
