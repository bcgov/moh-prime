# GetEnrolmentCertificate API Version 1 

The version currently in production.


## Sample Request 

`GET` request to `https://{host}/api/v1/provisioner-access/certificate/{guid}`

e.g. https://dev.pharmanetenrolment.gov.bc.ca/api/v1/provisioner-access/certificate/dadc9010-c380-4dab-968f-755c77a25mom

The `guid` parameter is part of a web page link that is in an email sent out from the PRIME application to Pharmanet administrators, as requested by PRIME enrollees.


## Sample Response 

```
{
    "result": {
        "firstName": "PRIMET",
        "lastName": "THIRTYEIGHT",
        "preferredFirstName": null,
        "preferredMiddleName": null,
        "preferredLastName": null,
        "gpid": "2*!BHUMI.A%PS*M?5!58",
        "expiryDate": "2023-03-25T18:11:04.764577+00:00",
        "careSettings": [
            {
                "code": 2,
                "name": "Private Community Health Practice"
            }
        ],
        "group": 3
    }
}
```

# GetEnrolmentCertificate API Version 2

At time of writing, the revised API will function essentially the same, that is, requiring an expiring token.  The change will be that more data is returned in the response.  
