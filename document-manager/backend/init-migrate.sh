#!/bin/bash
# init-migrate.sh

set -e

# Wait for PostgreSQL database container to be ready for connection requests
# before running database migrations.
until PGPASSWORD=postgres psql -h "$DB_HOST" -U "postgres" -d postgres -v QueryTimeout=1 -v ON_ERROR_STOP=1 -c "select version()" > /dev/null;
do
    echo "Postgres is unavailable for connection requests..."
    sleep 2
done

# Execute Flask database migration via Alembic
echo "Postgres is up - ready for connection requests and query execution."
exec "$@"
