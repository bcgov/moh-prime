import os
import dotenv
import logging

envPath = os.path.join(os.path.dirname(__file__), '.env')
if os.path.exists(envPath):
    logging.info("loading dot env...")
    dotenv.load_dotenv()

ACCESS_KEY = os.environ["ACCESS_KEY"]
SECRET_KEY = os.environ["SECRET_KEY"]
S3_BUCKET_NAME = os.environ["S3_BUCKET_NAME"]
OBJSTOR_ENDPOINT = os.environ["OBJSTOR_ENDPOINT"]

ADMIN_EMAIL = os.environ["ADMIN_EMAIL"]
SMTP_SERVER = os.environ["SMTP_SERVER"]
