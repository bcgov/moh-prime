FROM python:3.12.4
SHELL ["/bin/bash","-c"]

# Update installation utility
RUN apt-get update -yqq

# Create working directory
RUN mkdir /app
WORKDIR /app

# Install the requirements
COPY requirements.txt .
RUN set -x && \
    pip3 install -r requirements.txt

COPY . .

# Run the server
EXPOSE 5001 9191
ENTRYPOINT ["./init-backend.sh"]
