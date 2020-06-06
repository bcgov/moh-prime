FROM docker-registry.default.svc:5000/dqszvc-tools/python-36-rhel7:1-36

# Update installation utility
RUN apt-get update

# Create working directory
RUN mkdir /app
WORKDIR /app

# Install the requirements
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

COPY . .

# Run the server
EXPOSE 5001
CMD ["flask","run"]
