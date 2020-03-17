#!/bin/bash
backup_dir="/opt/backup"
logfile="${backup_dir}/backup.log"
retentionNumber=7
MaxFileSize=2048
dateinfo=`date '+%Y-%m-%d %H:%M:%S'`
timestamp=`date +%s`

function fileRotate() {
    ls -tp ${backup_dir}/backup.*.tgz | tail -n +${retentionNumber} | xargs -I {} rm -- {}
    ls -tp ${logfile}.*.tgz | tail -n +${retentionNumber} | xargs -I {} rm -- {}
    ls -tp ${backup_dir}/metabase-database*.backup.tgz | tail -n +${retentionNumber} | xargs -I {} rm -- {}
    ls -tp ${logfile}.*.tgz | tail -n +${retentionNumber} | xargs -I {} rm -- {}
    ls -tp ${backup_dir}/postgres-database*.backup.tgz | tail -n +${retentionNumber} | xargs -I {} rm -- {}
    ls -tp ${logfile}.*.tgz | tail -n +${retentionNumber} | xargs -I {} rm -- {}
    echo "${dateinfo} - Logs rotated" >> ${logfile}
    echo "${dateinfo} - Logs rotated"
}

function databaseBackup() {
    backup_dir="/opt/backup"
    retentionNumber=7
    MaxFileSize=2048
    dateinfo=`date '+%Y-%m-%d %H:%M:%S'`
    timestamp=`date +%s`
    logfile="${backup_dir}/backup.log"
    cd ${backup_dir}
    echo "${dateinfo} - Starting backup of databases " >> ${logfile}
    file_size=`du -b ${logfile} | tr -s '\t' ' ' | cut -d' ' -f1`
    if [ "$file_size" -gt "$MaxFileSize" ]
    then   
        tar czf ${logfile}.${timestamp}.tgz ${logfile}
        touch ${logfile}
    fi
    PGPASSWORD=${POSTGRES_PASSWORD}
    /usr/bin/vacuumdb -z -h ${PGHOST} -U ${PGUSERNAME} ${PGDATABASE} >/dev/null 2>&1
    /usr/bin/pg_dump -U ${PGUSERNAME} -F c -b ${PGDATABASE} -h ${PGHOST} -f ${backup_dir}/${PGDATABASE}-database-${timestamp}.backup
    echo "${dateinfo} - Backup and Vacuum complete on ${dateinfo} for database: ${PGDATABASE} " >> ${logfile}
    tar czf ${backup_dir}/${PGDATABASE}-database-${timestamp}.backup.tgz ${backup_dir}/${PGDATABASE}-database-${timestamp}.backup 
    rm -f ${backup_dir}/${PGDATABASE}-database-${timestamp}.backup
    echo "${dateinfo} - Backup compressed for database: ${PGDATABASE} " >> ${logfile}
    echo "Starting backup of metabase" >> ${logfile}
    file_size=`du -b ${logfile} | tr -s '\t' ' ' | cut -d' ' -f1`
    if [ "$file_size" -gt "$MaxFileSize" ]
    then   
        tar czf ${logfile}.${timestamp}.tgz ${logfile}
        touch ${logfile}
    fi
    PGPASSWORD=${METABASE_PASSWORD}
    /usr/bin/vacuumdb -z -h ${METABASE_HOST} -U ${METABASE_USERNAME} ${METABASE_DATABASE} >/dev/null 2>&1
    /usr/bin/pg_dump -U ${METABASE_USERNAME} -F c -b ${METABASE_DATABASE} -h ${METABASE_HOST} -f ${backup_dir}/${METABASE_DATABASE}-database-${timestamp}.backup
    echo "${dateinfo} - Backup and Vacuum complete on ${dateinfo} for database: ${METABASE_DATABASE} " >> ${logfile}
    tar czf ${backup_dir}/${METABASE_DATABASE}-database-${timestamp}.backup.tgz ${backup_dir}/${METABASE_DATABASE}-database-${timestamp}.backup 
    rm -f ${backup_dir}/${METABASE_DATABASE}-database-${timestamp}.backup
    echo "${dateinfo} - Backup compressed for database: ${METABASE_DATABASE} " >> ${logfile}
    echo "${dateinfo} - Done backup of databases " >> ${logfile}
    echo "${dateinfo} Done backup of databases"
}

function dailyAction() {
    while true
    do 
        sleep 86400
        fileRotate
    done
}

function hourlyAction() {
    while true
    do 
        sleep 3600
        databaseBackup
    done
}

dailyAction &disown
hourlyAction &disown
