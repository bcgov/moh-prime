from json.decoder import JSONDecodeError
import sys, os, json, csv;

IS_THERE_MORE_DATA_FILE_LOCATION = "/tmp/isThereMoreData.txt"

json_as_dict = None
try:
    json_as_dict = json.load(sys.stdin);
except JSONDecodeError:
    print('Non-JSON response from API.', file=sys.stderr)
    # Remove in case it exists from previous loop iteration 
    os.remove(IS_THERE_MORE_DATA_FILE_LOCATION)
    sys.exit(1)

output = csv.writer(sys.stdout);
# Using Standard Error despite following not being an error so as to not interfere with Standard Output/Input expected by downstream process 
print('Converting JSON to CSV ...', file=sys.stderr)
# Note that the columns in the CSV output need to match the invocation of the Postgres COPY command
# Ultimately the `pnetTransactions` JSON fields need to be in the expected order 
for row in json_as_dict['pnetTransactions']:
    # Remove any leading and trailing whitespace
    if row['providerSoftwareId'] is not None:
        row['providerSoftwareId'] = row['providerSoftwareId'].strip()
    if row['providerSoftwareVer'] is not None:
        row['providerSoftwareVer'] = row['providerSoftwareVer'].strip()
    output.writerow(row.values());

isThereMoreData = json_as_dict['isThereMoreData']
# Using Standard Error despite following not being an error so as to not interfere with Standard Output/Input expected by downstream process 
print(f"Writing to file:  Is there more data?  {isThereMoreData}", file=sys.stderr)
# Let external process know whether there are more results according to JSON response
f = open(IS_THERE_MORE_DATA_FILE_LOCATION, "w")
f.write(isThereMoreData)
f.close()
