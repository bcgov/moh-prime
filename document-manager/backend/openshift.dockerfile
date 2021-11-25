FROM public.ecr.aws/bitnami/python:3.6-prod

WORKDIR /opt/app-root/src

ENV LANG=C.UTF-8
ENV LC_ALL=C.UTF-8

# Install dependencies
RUN install_packages gcc libc6-dev libpq-dev libmagic-dev

# Install the requirements
COPY ./requirements.txt .
RUN pip install wheel && \
    pip install -r requirements.txt --src /opt/app-root/src

RUN apt-get purge -y --auto-remove gcc libc6-dev

COPY . .

ENV FLASK_APP app.py

# Run the server
EXPOSE 5001 9191
ENTRYPOINT /opt/app-root/src/app.sh backend
