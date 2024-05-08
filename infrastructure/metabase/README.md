# UPGRADE the version of Metabase if you’re running 45 or below

### 1. Download the latest version of Metabase image from docker hub

`docker pull metabase/metabase:latest`

### 2. Login to OpenShift
`oc login <token>`

### 3. Switch to the desired project 
`oc project <namescapce>`

### 4. Tag the downloaded image

`docker tag metabase/metabase:latest image-registry.apps.silver.devops.gov.bc.ca/<namespace>/metabase:<image-version>`

### 5. Login to docker and Push the image to the metabase imagestream in tools namespace

`docker login -u 'oc whoami' -p <password> image-registry.apps.silver.devops.gov.bc.ca/<namespace>`

`docker push image-registry.apps.silver.devops.gov.bc.ca/<namespace>/metabase:<image-version>`

### 6. Point the latest image in metabase imagestream to the newly downloaded image

`oc tag metabase:v0.47.2 metabase:latest`


