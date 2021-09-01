import sys, json, csv;

json_as_dict = json.load(sys.stdin);

output = csv.writer(sys.stdout);

for row in json_as_dict['pnetTransactions']:
    output.writerow(row.values());


