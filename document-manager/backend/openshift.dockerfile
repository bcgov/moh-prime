FROM python:3.12.4

WORKDIR /opt/app-root/src/

# Install dependencies
RUN apt-get update -yqq && \
    apt-get install -y gcc libc6-dev libpq-dev libmagic-dev && \
    rm -rf /var/lib/apt/lists/*


# Install the requirements
COPY ./requirements.txt .
RUN pip install wheel && \
    pip install -r requirements.txt --src /opt/app-root/src

RUN apt-get purge -y --auto-remove gcc libc6-dev

COPY . .
# 4. CRITICAL FOR OPENSHIFT: Fix permissions for random user execution
RUN chgrp -R 0 /opt/app-root && \
    chmod -R g=u /opt/app-root && \
    chmod +x /opt/app-root/src/app.sh

ENV FLASK_APP app.py

# Run the server
EXPOSE 5001 9191
ENTRYPOINT ["./app.sh", "backend"]
