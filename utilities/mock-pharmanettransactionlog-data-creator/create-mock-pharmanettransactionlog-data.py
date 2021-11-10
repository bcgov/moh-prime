import sys
import datetime
import random

import csv
from faker import Faker


num_logs = int(sys.argv[1])
output_filename = sys.argv[2]
starting_tx_id = int(sys.argv[3])

faker = Faker()
with open(output_filename, mode='w') as data_file:
    data_writer = csv.writer(data_file, delimiter=',', quotechar='"')
    print(f"Started generation of {num_logs} records at {datetime.datetime.now()}")

    tx_id = starting_tx_id
    for _ in range(num_logs):
        pract_id_format = '#' * random.randint(3, 5)

        # HNSecure data may not have Transaction ID but not simulating that 
        data_writer.writerow([tx_id, faker.date_time_this_year(), faker.user_name(), faker.ipv4(), random.choice([faker.ipv4(), '']), 
            random.choice([faker.pystr(3, 3), faker.pystr(10, 10)]).upper(), random.choice(['NEXT', 'TDR', 'TIL', 'TRS', 'TRX', 'TRP,TRX']),
            random.choice(['0', 'X0', 'X1', 'X4', '']), faker.pystr_format(string_format=pract_id_format), random.choice(['P1', '91', '96']),
            random.choice(['0', '']), random.choice(['KC', 'M1', 'P1', 'PE']), random.choice(['2', '4', '13'])])
        tx_id += 1

    print(f"Completed generation of records into {output_filename} at {datetime.datetime.now()}")

