# Backup service

The backup service is used to perform database backups for our Postgres databases: the prime-prod (or dev, or test) database, which is the primary data store for PRIME and for storing data for Metabase for reporting purposes.

**Made obsolete in favor of https://github.com/BCDevOps/backup-container which is running in PRIME Production**


## How to



##### Make changes

The backup service comprises of a few different components


###### 1. backup.cron 
a cron file  to indicate the frequency of when the backup script runs
###### 2. backup.sh 
the script that performs the backups, using pgdump
###### 3. (UNTESTED) docker-compose.yml 
a Docker compose file, possibly to run the backup service locally. This has not been tested as of May 25th, 2023. We are moving towards having this service in dev, test, and prod in the interim. 
###### 4. entrypoint.sh 
script for Dockerfile to run to start running the container
###### 5. (UNTESTED)  Dockerfile 
possibly used by docker compose file to run locally
###### 6. openshift.dockerfile 
The Dockerfile used for deploying into Openshift
###### 7. restore.sh 
a script that outputs commands to perform a database restore. 

Make your changes relative to what you're trying to do (i.e. if you need to make changes to how the service is built, put that in the Dockerfile)
 
##### Build and deploy

Run the appropriate build and deploy job in GitHub Actions (pending completion of https://bcgovmoh.atlassian.net/browse/PRIME-2489)
