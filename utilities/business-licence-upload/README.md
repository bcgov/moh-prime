Execute like this:

`docker run -it --rm --name PRIME-1583 -e KEYCLOAK_URL=<KEYCLOAK_URL> -e KEYCLOAK_REALM=<KEYCLOAK_REALM> -e KEYCLOAK_CLIENT_ID=<KEYCLOAK_CLIENT_ID> -e KEYCLOAK_CLIENT_SECRET=<KEYCLOAK_CLIENT_SECRET> -e DOCMAN_DOCUMENTS_URL=https://pr-1293.pharmanetenrolment.gov.bc.ca/api/docman/documents -v "<HOST_SRC_DIR>:/usr/src/myapp" -w /usr/src/myapp python:3.9.5 bash upload-business-licences.sh <FILE_TO_UPLOAD>`

Where:

| Parameter              | Notes          
| ---------------------- | ------------- 
| KEYCLOAK_URL           | e.g. `https://dev.oidc.gov.bc.ca/auth` 
| KEYCLOAK_REALM         | e.g. `v4mbqqas`      
| KEYCLOAK_CLIENT_ID     | See James H if necessary     
| KEYCLOAK_CLIENT_SECRET | See James H if necessary
| DOCMAN_DOCUMENTS_URL   | URL of Document Manager's `documents` end-point, e.g. `https://pr-1293.pharmanetenrolment.gov.bc.ca/api/docman/documents` 
| HOST_SRC_DIR           | Where this README is located on host machine, e.g. `C:\Users\177092\Source\Repos\moh-prime\utilities\business-licence-upload` 
| FILE_TO_UPLOAD         | Name of file to upload, relative to this directory 
