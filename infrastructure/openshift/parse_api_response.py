import sys, json, csv;

json_as_dict = json.load(sys.stdin);

output = csv.writer(sys.stdout);
# Convert JSON to CSV
# Note that the columns in the CSV output need to match the invocation of the Postgres COPY command
# Ultimately the `pnetTransactions` JSON fields need to be in the expected order 
for row in json_as_dict['pnetTransactions']:
    output.writerow(row.values());

# Let external process know whether there are more results according to JSON response
f = open("/tmp/isThereMoreData.txt", "w")
f.write(json_as_dict['isThereMoreData'])
f.close()
