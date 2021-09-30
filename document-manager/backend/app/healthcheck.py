from .config import Config

from flask import current_app
from psycopg2 import OperationalError
from redis import Redis, ConnectionError
from sqlalchemy import create_engine


def postgres_healthcheck():
    """
    Verify that the PRIME PostgreSQL database is available for connection requests.
    If successful, return True. Otherwise, return False.
    """
    # DB_HOST = Config.DB_HOST
    # DB_PORT = Config.DB_PORT
    # DB_NAME = Config.DB_NAME
    # DB_USER = Config.DB_USER
    # DB_PASS = Config.DB_PASS

    try:
        # db = f"postgres+psycopg2://{DB_USER}:{DB_PASS}@{DB_HOST}:{DB_PORT}/{DB_NAME}"
        db = ""
        db_engine = create_engine(db)

        db_engine.connect()
    except OperationalError:
        return False, "Document Manager is unhealthy because the PostgreSQL database cannot be reached."
    
    return True, "Document Manager is able to connect to the PostgreSQL database."


def redis_healthcheck():
    """
    Verify Redis is available for connection requests.
    """
    REDIS_HOST = Config.CACHE_REDIS_HOST
    REDIS_PORT = Config.CACHE_REDIS_PORT
    REDIS_PASS = Config.CACHE_REDIS_PASS

    redis_connect = Redis(host=REDIS_HOST, port=REDIS_PORT, password=REDIS_PASS)

    # Attempt connection to Redis via a ping. If it succeeds, return True.
    # Otherwise, return False upon a connection error.
    try:
        redis_connect.ping()
    except ConnectionError:
        return False, "Document Manager is unhealthy because Redis cannot be reached."

    return True, "Document Manager is able to connect to Redis."
