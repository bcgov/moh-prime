from .config import Config

from psycopg2 import connect, OperationalError
from redis import Connection, ConnectionError

# Config constants
DB_HOST = Config.DB_HOST
DB_PORT = Config.DB_PORT
DB_NAME = Config.DB_NAME
DB_USER = Config.DB_USER
DB_PASS = Config.DB_PASS
REDIS_HOST = Config.CACHE_REDIS_HOST
REDIS_PORT = Config.CACHE_REDIS_PORT
REDIS_PASS = Config.CACHE_REDIS_PASS


# Functions
def postgres_healthcheck():
    """
    Verify that the PRIME PostgreSQL database is available for connection requests.
    If successful, return True. Otherwise, return False.
    """

    try:
        db_conn = connect(dbname=DB_NAME,
                          user=DB_USER,
                          password=DB_PASS,
                          host=DB_HOST,
                          port=DB_PASS,
                          connect_timeout=30
                          )
        db_cur = db_conn.cursor()
        db_cur.execute("SELECT 1")
        db_conn.close()
    except OperationalError:
        return False, "Document Manager is unhealthy because the PostgreSQL database cannot be reached."
    
    return True, "Document Manager is able to connect to the PostgreSQL database."


def redis_healthcheck():
    """
    Verify Redis is available for connection requests.
    """
    redis_connect = Connection(host=REDIS_HOST, port=REDIS_PORT, password=REDIS_PASS)

    # Attempt connection to Redis via a ping. If it succeeds, return True.
    # Otherwise, return False upon a connection error.
    try:
        redis_connect.check_health()
    except ConnectionError:
        return False, "Document Manager is unhealthy because Redis cannot be reached."

    return True, "Document Manager is able to connect to Redis."
