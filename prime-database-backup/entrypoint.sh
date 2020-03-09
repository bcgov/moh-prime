#!/bin/bash
backup_dir="/opt/backup"
logfile="${backup_dir}/backup.log"
echo "Unauthorized access prohibited."
/opt/backup.sh
tail -f ${logfile}