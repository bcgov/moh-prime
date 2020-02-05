#!/bin/bash
logfile="/opt/backup/pgsql.log"
backup_dir="/opt/backup"
touch $logfile
databases=`psql -h localhost -U postgres -q -c "\l" | sed -n 4,/\eof/p | grep -v rows\) | grep -v template0 | grep -v template1 | awk {'print $1'}`

echo "Starting backup of databases " >> $logfile
for i in $databases; do
        dateinfo=`date '+%Y-%m-%d %H:%M:%S'`
        timeslot=`date '+%Y%m%d%H%M'`
        /usr/bin/vacuumdb -z -h localhost -U postgres $i >/dev/null 2>&1
        /usr/bin/pg_dump -U postgres -i -F c -b $i -h 127.0.0.1 -f $backup_dir/$i-database-$timeslot.backup
        echo "Backup and Vacuum complete on $dateinfo for database: $i " >> $logfile
done
echo "Done backup of databases " >> $logfile

#tail -15 /backup/pgsql.log | mailx youremail@domain.com