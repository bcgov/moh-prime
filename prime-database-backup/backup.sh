#!/bin/bash
logfile="/opt/backup/pgsql.log"
backup_dir="/opt/backup"
touch $logfile

echo "Starting backup of databases " >> $logfile
function databaseBackup() {
        dateinfo=`date '+%Y-%m-%d %H:%M:%S'`
        timeslot=`date '+%Y%m%d%H%M'`
        /usr/bin/vacuumdb -z -h ${PGHOST} -U ${PGUSERNAME} ${PGDATABASE} >/dev/null 2>&1
        /usr/bin/pg_dump -U ${PGUSERNAME} -F c -b ${PGDATABASE} -h ${PGHOST} -f $backup_dir/${PGDATABASE}-database-$timeslot.backup
        echo "${timeslot} - Backup and Vacuum complete on $dateinfo for database: ${PGDATABASE} " >> $logfile
        tar -czf $backup_dir/${PGDATABASE}-database-$timeslot.backup.tgz $backup_dir/${PGDATABASE}-database-$timeslot.backup
        rm -f $backup_dir/${PGDATABASE}-database-$timeslot.backup
        dateinfo=`date '+%Y-%m-%d %H:%M:%S'`
        timeslot=`date '+%Y%m%d%H%M'`
        echo "${timeslot} - Backup compressed $dateinfo for database: ${PGDATABASE} " >> $logfile
}
databaseBackup
echo "Done backup of databases " >> $logfile

#tail -15 /backup/pgsql.log | mailx youremail@domain.com