import sys, json, csv;

json_as_dict = json.load(sys.stdin);

output = csv.writer(sys.stdout);
# Convert JSON to CSV
for row in json_as_dict['pnetTransactions']:
    output.writerow(row.values());

# Let external process know whether there are more results according to JSON response
f = open("/tmp/isThereMoreData.txt", "w")
f.write(json_as_dict['isThereMoreData'])
f.close()
