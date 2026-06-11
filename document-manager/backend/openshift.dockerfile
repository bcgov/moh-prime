FROM python:3.12-slim

WORKDIR /app

# Install dependencies
RUN apt-get update -yqq && \
    apt-get install -y gcc libc6-dev libpq-dev libmagic-dev && \
    rm -rf /var/lib/apt/lists/*

# Install the requirements
COPY ./requirements.txt .
RUN pip install --no-cache-dir wheel && \
    pip install --no-cache-dir -r requirements.txt

RUN apt-get purge -y --auto-remove gcc libc6-dev

COPY . .

ENV FLASK_APP app.py

# Run the server
EXPOSE 5001 9191
ENTRYPOINT ["./app.sh", "backend"]
