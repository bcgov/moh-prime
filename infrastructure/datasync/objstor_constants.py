import os
import dotenv

envPath = os.path.join(os.path.dirname(__file__), '.env')
if os.path.exists(envPath):
    print("loading dot env...")
    dotenv.load_dotenv()

ACCESS_KEY = os.environ["ACCESS_KEY"]
SECRET_KEY = os.environ["SECRET_KEY"]
S3_BUCKET_NAME = os.environ["S3_BUCKET_NAME"]
OBJSTOR_ENDPOINT = os.environ["OBJSTOR_ENDPOINT"]

ADMIN_EMAIL = os.environ["ADMIN_EMAIL"]
SMTP_SERVER = os.environ["SMTP_SERVER"]


def print_constants():
    print(f"ACCESS_KEY: {ACCESS_KEY}")
    print(f"SECRET_KEY: {SECRET_KEY}")
    print(f"S3_BUCKET_NAME: {S3_BUCKET_NAME}")
    print(f"OBJSTOR_ENDPOINT: {OBJSTOR_ENDPOINT}")