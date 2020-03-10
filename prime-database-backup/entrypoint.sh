#!/bin/bash
backup_dir="/opt/backup"
logfile="${backup_dir}/backup.log"
touch ${logfile}
echo "Unauthorized access prohibited."
/opt/backup.sh
tail -F ${logfile}
tail -F /dev/null