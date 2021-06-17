Although this is named "business-licence-upload", it is applicable for bulk uploading organization agreements as well;  
simply the values of the parameters for the script are changed.  

To enable a consistent execution environment, we use a Docker container for our purpose.

Execute like this:

`docker run -it --rm --name PRIME-1583 -e KEYCLOAK_URL=<KEYCLOAK_URL> -e KEYCLOAK_REALM=<KEYCLOAK_REALM> -e KEYCLOAK_CLIENT_ID=<KEYCLOAK_CLIENT_ID> -e KEYCLOAK_CLIENT_SECRET=<KEYCLOAK_CLIENT_SECRET> -e DOCMAN_DOCUMENTS_URL=<DOCMAN_DOCUMENTS_URL> -e DOCMAN_DEST_DIR=<business_licences|signed_org_agreements> -e OUTPUT_FILENAME=<OUTPUT_FILENAME> -v "<HOST_SRC_DIR>:/usr/src/myapp" -v "<DATA_DIR>:/usr/data" -w /usr/src/myapp python:3.9.5 bash upload-business-licences.sh /usr/data`

Where:

| Parameter              | Notes          
| ---------------------- | ------------- 
| KEYCLOAK_URL           | e.g. `https://dev.oidc.gov.bc.ca/auth` 
| KEYCLOAK_REALM         | e.g. `v4mbqqas`      
| KEYCLOAK_CLIENT_ID     | See James H if necessary     
| KEYCLOAK_CLIENT_SECRET | See James H if necessary
| DOCMAN_DOCUMENTS_URL   | URL of Document Manager's `documents` end-point, e.g. `https://pr-1293.pharmanetenrolment.gov.bc.ca/api/docman/documents` 
| HOST_SRC_DIR           | Where this README is located on host machine, e.g. `C:\Users\177092\Source\Repos\moh-prime\utilities\business-licence-upload` 
| DATA_DIR               | On host machine, where the directory of scanned business licence/organization agreements files is located, e.g. `C:\Users\177092\Downloads\prime-dummy-biz-licences`
| DOCMAN_DEST_DIR        | For scanned business licences, `business_licences` or organization agreements, `signed_org_agreements`
| OUTPUT_FILENAME        | e.g. `business_licences_uploaded.csv` 


The output file is created in the same directory as this README.  FYI, on the Document Manager server, the uploaded files can be found under `/app/document_uploads/business_licences` on its file system, for example.
