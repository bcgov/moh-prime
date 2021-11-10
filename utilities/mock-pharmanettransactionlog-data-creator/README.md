# mock-pharmanettransactionlog-data-creator README

To enable a consistent execution environment, we use a Docker container for our purpose.

## How to execute

`docker run -it --rm --name PRIME-1939 -v "<HOST_SRC_DIR>:/usr/src/myapp" -w /usr/src/myapp python:3.9.7 /bin/bash -c "pip install Faker; python create-mock-pharmanettransactionlog-data.py <NUM_RECORDS> <OUTPUT_FILENAME> <INITIAL_TRANSACTION_ID>"`

Where:

| Parameter              | Notes          
| ---------------------- | ------------- 
| HOST_SRC_DIR           | Where this README is located on host machine, e.g. `C:\Users\177092\Source\Repos\moh-prime\utilities\mock-pharmanettransactionlog-data-creator` 
| NUM_RECORDS            | e.g. 30000000
| OUTPUT_FILENAME        | e.g. "pnet-logs.csv"
| INITIAL_TRANSACTION_ID | e.g. 1000000

The output file is created in the same directory as this README.  


## How to load into `PharmanetTransactionLog` table

Assuming you don't have the `psql` client locally:

1. Upload .csv file to any web-api pod, e.g. `oc cp .\pnet-logs.csv pr-1722-webapi-33-qkcf7:/tmp/pnet-logs.csv` 

2. Use `psql` in the pod to COPY into target database, e.g. importing into the `dev` Patroni cluster, `prime-pr-1722` database:
```
psql -h dev-patroni -U prime_user -c "\COPY \"PharmanetTransactionLog\"(\"TransactionId\", \"TxDateTime\", \"UserId\", \"SourceIpAddress\", \"LocationIpAddress\", \"PharmacyId\", \"TransactionType\", \"TransactionSubType\", \"PractitionerId\", \"CollegePrefix\", \"TransactionOutcome\", \"ProviderSoftwareId\", \"ProviderSoftwareVersion\")
    FROM '/tmp/pnet-logs2.csv'
    csv;" prime-pr-1721
```

3. The password for `prime_user` can be found in the OpenShift secret `dev-patroni-secret`, `app-db-password` key


# Other useful commands

These require some tweaking for the given situation:

1.  To generate multiple files, see `loop-generation.ps1`

2.  To upload multiple files to a OpenShift pod, see `loop-upload.ps1`

3.  To load data from multiple files, working in an OpenShift pod (be sure to be using `bash`):

```
for i in {4..8}; do PGPASSWORD=<PASSWORD GOES HERE> psql -h dev-patroni -U prime_user -c "\COPY \"PharmanetTransactionLog\"(\"TransactionId\", \"TxDateTime\", \"UserId\", \"SourceIpAddress\", \"LocationIpAddress\", \"PharmacyId\", \"TransactionType\", \"TransactionSubType\", \"PractitionerId\", \"CollegePrefix\", \"TransactionOutcome\", \"ProviderSoftwareId\", \"ProviderSoftwareVersion\")
    FROM '/tmp/pnet-logs${i}.csv'
    csv;" prime_dev ; done
```

4.  To load data from multiple files, working locally (e.g. in `~/Source/Repos/moh-prime/utilities/mock-pharmanettransactionlog-data-creator`) using `bash`:

```
echo $(date);   for i in {1..5}; do PGPASSWORD=<PASSWORD GOES HERE> psql -h localhost -p 15432 -U prime-user -c "\COPY \"PharmanetTransactionLog\"(\"TransactionId\", \"TxDateTime\", \"UserId\", \"SourceIpAddress\", \"LocationIpAddress\", \"PharmacyId\", \"TransactionType\", \"TransactionSubType\", \"PractitionerId\", \"CollegePrefix\", \"TransactionOutcome\", \"ProviderSoftwareId\", \"ProviderSoftwareVersion\")
    FROM 'pnet-logs${i}.csv'
    csv;" prime-test ; done;   echo $(date)
```
