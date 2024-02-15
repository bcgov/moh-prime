# Backup Deployments of Postgres
From GitHub -bcgov/backup-container, copy openshift/templates, config and docker folders. Together, the scripts and templates which will automatically deploy the backup app are provided in the openshift/templates directory. 
The following outlines the deployment of a simple backup of a PostgreSQL database in the 9c33a9-dev project namespace, on OCP v4.x.

<summary>Build backup-container image</summary>

In order to build an image and buildConfig in openshift, the backup-build.yaml file in backup directory is used. 
1. The value of GIT_REPO_URL, GIT_REF and SOURCE_CONTEXT_DIR in backup-build.yaml file needs to be modified.  (GIT_REPO_URL: https://github.com/bcgov/moh-prime.git, GIT_REF : s3storage, SOURCE_CONTEXT_DIR : /infrastructure/backup-container/docker) 

2. `git clone https://github.com/bcgov/moh-prime.git && cd infrastructure\backup-container`.

Create the image and tag the image.

```bash
oc login --token=<token>
oc -n 9c33a9-tools process -f ./openshift/templates/backup/backup-build.yaml \
  -p NAME=backup-container OUTPUT_IMAGE_TAG=dev| oc -n 9c33a9-tools create -f -

oc tag --alias=true backup-container:latest backup-container:dev
```
Build a new image

```bash
oc start-build -n 9c33a9-tools backup-container
```

<summary>Configure your DBs and set the cron schedule</summary>
. Configure (./config/backup.conf) (listing your database(s) and setting your cron schedule).

```bash
postgres=dev-patroni:5432/prime_dev

0 1 * * * default bash ./backup.sh -s
0 4 * * * default bash ./backup.sh -s -v all
```

<summary>Deploy the app</summary>

1. Configure references to your DB credentials in [backup-deploy.yaml](./openshift/templates/backup/backup-deploy.yaml), replacing the boilerplate `DATABASE_USER` and `DATABASE_PASSWORD` environment variables.

```yaml
- name: DATABASE_USER
  valueFrom:
    secretKeyRef:
      name: "${DATABASE_DEPLOYMENT_NAME}"
      key: "${DATABASE_USER_KEY_NAME}"
- name: DATABASE_PASSWORD
  valueFrom:
    secretKeyRef:
      name: "${DATABASE_DEPLOYMENT_NAME}"
      key: "${DATABASE_PASSWORD_KEY_NAME}"
```

Note that underscores should be used in the environment variable names.

2. Create your customized `./openshift/backup-deploy.overrides.param` parameter file, if required.

3. In this example, the namespace is `9c33a9-dev` and the app name is `backup-container`:
If you would like to utilize s3 storage and store backup files in a s3 bucket, create a secret in openshift for s3. 
Note that the key names within the database secret referencing database username and password are `app-db-username` and `app-db-password`, respectively. If this is not the case for your deployment, specify the correct key names as parameters `DATABASE_USER_KEY_NAME` and `DATABASE_PASSWORD_KEY_NAME`. Also note that `BACKUP_VOLUME_CLASS` is netapp-file-standard since the backup files are stored in S3 storage.
 the key names within the s3 secret referencing s3 username, password, endpoint and bucket are `S3_USER`, `S3_PASSWORD``, `S3_ENDPOINT`, and `S3_BUCKET`, respectively.


```bash
oc -n 9c33a9-dev create configmap backup-conf --from-file=./config/backup.conf
oc -n 9c33a9-dev label configmap backup-conf app=backup-container

oc -n 9c33a9-dev process -f ./openshift/templates/backup/backup-deploy.yaml \
-p NAME=backup-container \
-p IMAGE_NAMESPACE=9c33a9-tools -p SOURCE_IMAGE_NAME=backup-container -p TAG_NAME=dev \
-p BACKUP_VOLUME_NAME=backup-container -p BACKUP_VOLUME_SIZE=5Gi -p BACKUP_VOLUME_CLASS=netapp-file-standard \
-p VERIFICATION_VOLUME_SIZE=5Gi -p ENVIRONMENT_FRIENDLY_NAME='PRIME DB Backups' \
-p CPU_REQUEST=50m -p CPU_LIMIT=250m -p MEMORY_REQUEST=128Mi -p MEMORY_LIMIT=256Mi -p DATABASE_DEPLOYMENT_NAME=dev-patroni-secret -p DATABASE_USER_KEY_NAME=app-db-username -p DATABASE_PASSWORD_KEY_NAME=app-db-password -p ENVIRONMENT_NAME=9c33a9-dev | oc -n 9c33a9-dev create -f -

```

<summary>clean up the deployment</summary>

```bash
oc -n 9c33a9-dev delete pvc/backup-container pvc/backup-verification secret/backup-container secret/ftp-secret secret/S3-secret dc/backup-container networkpolicy/backup-container configmap/backup-conf
```

To clean up the image stream and build configuration

```bash
oc -n 9c33a9-tools delete buildconfig/backup-container imagestream/backup-container
```