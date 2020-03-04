#!/bin/bash
echo "Restores can be destructive processes, therefore they should only be manually performed by skilled, trained personnel."
echo "Replace the full, correct filename and path after '-v' to perform a backup restore:"
echo '$ tar -C / -zxvf $backup_dir/${PGDATABASE}-database-$timeslot.backup.tgz'
echo '$ pg_restore -U ${PGUSERNAME} -h ${PGHOST} -p 5432 -d ${PGDATABASE} -v "$backup_dir/${PGDATABASE}-database-$timeslot.backup"'