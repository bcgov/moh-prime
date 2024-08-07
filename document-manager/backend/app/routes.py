from app.extensions import api
from .config import Config
from .healthcheck import *

from flask_jwt_oidc.exceptions import AuthError
from flask_restx import Resource
from healthcheck import HealthCheck


def run_healthcheck():
    """Execute healthcheck function for Document Manager to verify if it is healthy.

    Document Manager relies on two services being available for it to function as designed: Redis and PostgreSQL. If
    one is not available, or if neither are, then a ConnectionError is thrown.

    Successful calls will return a successful attempt at connection.
    """
    healthcheck = HealthCheck()

    # Verify PostgreSQL and Redis status checks
    healthcheck.add_check(postgres_healthcheck)
    healthcheck.add_check(redis_healthcheck)

    return healthcheck


def register_routes(app):
    # Create py-healthcheck object for healthcheck endpoint
    docman_healthcheck = run_healthcheck()
    
    # Set URL rules for resources
    app.add_url_rule("/", endpoint="index")
    app.add_url_rule("/healthcheck", endpoint="healthcheck", view_func=docman_healthcheck.run)

    # Global Handlers
    @api.errorhandler(AuthError)
    def jwt_oidc_auth_error_handler(error):
        return {
            'status': getattr(error, 'status_code', 401),
            'message': str(error),
        }, getattr(error, 'status_code', 401)

    @api.errorhandler(Exception)
    def default_error_handler(error):
        if getattr(error, 'code', 500) == 500:
            current_app.logger.error(str(error))
        return {
            'status': getattr(error, 'code', 500),
            'message': str(error),
        }, getattr(error, 'code', 500)
