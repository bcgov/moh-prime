# PRIME - Document Manager API

The project uses a Python 3.6 runtime environment and [Flask REST-plus](https://flask-restplus.readthedocs.io/en/stable/) framework for the API.

The application uses SQLAlchemy as our ORM to interact with the database.

For the application directory structure, see [flask-RESTplus quickstart guide.](https://flask-restplus.readthedocs.io/en/stable/quickstart.html)

## Directory/Naming convention

The application directory is structured as follows:

```
|-- app
    |-- docman
        |-- models
        |-- resources
    |-- utils (Contains utility shared across modules)
|-- tests (Unit/Integration tests for the application)
|-- app.sh (Shell script used by the python OpenShift s2i image to run the application)
|-- Dockerfile (Dockerfile for running the application locally using Docker)
|-- Dockerfile.migrate (Dockerfile for running database migrations related to Document Manager locally using Docker)
|-- init.sh (Shell script used by Docker to locally deploy Flask web server)
|-- requirements.txt (Libraries required by the project)
```

## Pre-requisites and Installation

At PRIME, we deploy Document Manager in two components; a backend service, as well as a database migration service powered by Alembic. Both are locally deploy via `docker-compose` (see respective Dockerfiles for build/config/deploy steps), where the backend service is dependent on the database migration service running and completing. 

If you wish to deploy only Document Manager components, please follow the instructions below.


### Deployment of Document Manager components only

The application assumes you already have a working postgres DB with the required schema and tables and have the connection details in the .env file.

Follow the `.env-example` template to create an `.env` file with valid values before running the application.

A. OS Level Installation

- [Python 3.6](https://www.python.org/downloads/release/python-360/)
- [Pip](https://pypi.org/project/pip/)
- [Virtualenv](https://virtualenv.pypa.io/en/stable/)

1. Create a virtual environment with python 3.6 and activate it

```
virtualenv -p python3.6 .venv
source .venv/bin/activate
```

2. Install the requirements

```
pip install -r requirements.txt
```

3. Run flask migrations in the project

```
flask db upgrade
```

3. Run the application

```
flask run
```

B. Using a docker container

- [Docker](https://www.docker.com/)

1. Switch current directory to the project root

```
cd ../
```

2. Issue the makefile command that runs the backend

```
make backend
```

## Flask Click commands

Flask supports [click commands](http://flask.pocoo.org/docs/1.0/cli/) which lets you run one-off commands from the command line without having to run the complete app.

To see the list of all click commands, checkout `register_commands` method under the `__init__.py` file.

## Testing

The application uses `pytest` to run the tests and `coverage` to output the results. The testing structure is based around [flask testing documentation.](http://flask.pocoo.org/docs/1.0/testing/)

To run tests, use the following command:

```
coverage run --branch --source=app -m pytest
```
