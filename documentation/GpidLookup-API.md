# GpidLookup API

A GpidLookup API client will obtain their own account from the Ministry of Health, associated with a Keycloak instance.
Credentials consist of an account ID and secret.

PRIME will log each request to the API, including the API client details and request parameters, in a
secure database.  The retrieved data will also be logged.  This is meant to prevent misuse of this API.


## Step 1:  Obtain JWT Bearer Token

Connect to Keycloak instance associated with the PRIME environment, passing credentials assigned to your system.  E.g.

```
curl --location --request POST 'https://common-logon-test.hlth.gov.bc.ca/auth/realms/moh_applications/protocol/openid-connect/token' \
--header 'Content-Type: application/x-www-form-urlencoded' \
--data-urlencode 'grant_type=client_credentials' \
--data-urlencode 'client_id=IDIDIDIDIDIDIDIDIDID' \
--data-urlencode 'client_secret=SSSSSSSSSSSSSSSSSSSS'
```

The response will be something like this, with the token embedded:

```
{
    "access_token": "eyTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT",
    "expires_in": 300,
    "refresh_expires_in": 0,
    "token_type": "Bearer",
    "not-before-policy": 1605659926,
    "scope": "profile email"
}
```


## Step 2:  Call GpidLookup API with details about a PRIME enrollee

In the call to this API, pass details about a PRIME enrollee and interested Care Setting, as well as the token obtained in the previous step, e.g.:

```
curl -v -X GET --location 'https://dev.pharmanetenrolment.gov.bc.ca/api/v1/provisioner-access/gpid-lookup' \
--header 'Content-Type: application/json' \
--header 'Authorization: Bearer eyTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT' \
--data-raw '{
    "Gpid": "H86$J0C3Z$6DYHDFUZ@N",
    "FirstName": "PRIMET",
    "LastName": "FIFTEEN",
    "CareSetting": "PCHP"
}'
```

The name parameters will be compared against BCSC and Preferred names in the PRIME database.
Information will be returned if and only if all four parameters match data in the PRIME database.

If a BCSC user has not submitted the enrollment yet or never enrolled:
```
{
    "result": [
        {
            "gpid": null,
            "status": "User not found in PRIME, refer user to PRIME",
            "accessType": null,
            "licences": null
        }
    ]
}
```

When provided careSetting value does not match care settings of enrollee:
```
{
    "result": [
        {
            "gpid": null,
            "status": "Care Setting not selected, refer user to PRIME",
            "accessType": null,
            "licences": null
        }
    ]
}
```

PRIME administrators may not want enrollee information to be returned:
```
{
    "result": [
        {
            "gpid": null,
            "status": "None",
            "accessType": null,
            "licences": null
        }
    ]
}
```

If the PRIME enrollment is not finished, the following is returned:
```
{
    "result": [
        {
            "gpid": null,
            "status": "Incomplete enrollment, refer user to PRIME",
            "accessType": null,
            "licences": null
        }
    ]
}
```

In the case of an indefinite absence (absence From date provided, starting today or in the past, but no To date given):
```
{
    "result": [
        {
            "gpid": "H86$J0C3Z$6DYHDFUZ@N",
            "status": "Indefinite absence, deprovision user",
            "accessType": null,
            "licences": null
        }
    ]
}
```

When the enrollee is *currently* absent (both absent From and To dates provided):
```
{
    "result": [
        {
            "gpid": "H86$J0C3Z$6DYHDFUZ@N",
            "status": "Enrollee is in defined absence period, deactivate user. Call again the day after <absence end date>",
            "accessType": null,
            "licences": null
        }
    ]
}
```

For enrollees that have their renewal period expired and have not renewed:
```
{
    "result": [
        {
            "gpid": "H86$J0C3Z$6DYHDFUZ@N",
            "status": "User past renewal, refer user to PRIME",
            "accessType": null,
            "licences": null
        }
    ]
}
```
In the case the enrollee is past their renewal period and has also reported an indefinite absence, PRIME will return a status of `User past renewal and Indefinite absence, deprovision user.` 

When none of the previously mentioned conditions is the case, the response will contain details to provision each enrollee that has signed a Terms of Access (TOA) agreement, when the given `careSetting` value matches the care setting of the enrollee as known in PRIME.  Example response:
```
{
    "result": [
        {
            "gpid": "H86$J0C3Z$6DYHDFUZ@N",
            "status": "Provision user",
            "accessType": "Independent User - with OBOs",
            "licences": [
                {
                    "practRefId": "91",
                    "collegeLicenseNumber": "00002",
                    "pharmaNetId": null,
                    "redacted": false
                }
            ]
        }
    ]
}
```

When the PRIME enrollment has changed from the information previously returned to the API client for the enrollee, the response will be similar to:
```
{
    "result": [
        {
            "gpid": "H86$J0C3Z$6DYHDFUZ@N",
            "status": "Enrollee information updated, update user",
            "accessType": "Independent User - with OBOs",
            "licences": [
                {
                    "practRefId": "91",
                    "collegeLicenseNumber": "98765",
                    "pharmaNetId": null,
                    "redacted": false
                }
            ]
        }
    ]
}
```

However if the relevant information is unchanged from the information previously returned to the API client for the enrollee, the response will be similar to:
```
{
    "result": [
        {
            "gpid": "H86$J0C3Z$6DYHDFUZ@N",
            "status": "No change to enrollee information, do nothing",
            "accessType": null,
            "licences": null
        }
    ]
}
```

Lastly, due to privacy issues, in the very rare cases that a PRIME enrollee has more than one licence, for each licence, the licence-related information would be blanked-out and a licence-level Boolean field `redacted` would be set to `true`, e.g.
```
{
    "result": [
        {
            "gpid": "H86$J0C3Z$6DYHDFUZ@N",
            "status": "Contact PRIME Support",
            "accessType": "Independent User - with OBOs",
            "licences": [
                {
                    "practRefId": null,
                    "collegeLicenseNumber": null,
                    "pharmaNetId": null,
                    "redacted": true
                },
                {
                    "practRefId": null,
                    "collegeLicenseNumber": null,
                    "pharmaNetId": null,
                    "redacted": true
                }
            ]
        }
    ]
}
```


## Appendix

|Possible value for `CareSetting` input parameter|Human-readable definition|
|------------------------------------------------|-------------------------|
|PCHP|Private Community Health Practice|
|CP|Community Pharmacy|
|NHA|Northern Health Authority|
|IHA|Interior Health Authority|
|VCHA|Vancouver Coastal Health Authority|
|VIHA|Vancouver Island Health Authority|
|FHA|Fraser Health Authority|
|PHSA|Provincial Health Services Authority|


|Possible values for `status` output                |
|---------------------------------------------------|
|User not found in PRIME, refer user to PRIME       | 
|Care Setting not selected, refer user to PRIME     |
|None                                               |
|Incomplete enrollment, refer user to PRIME         |
|User past renewal, refer user to PRIME             | 
|Indefinite absence, deprovision user               |
|Enrollee is in defined absence period, deactivate user.  Call again the day after &lt;absence end date&gt;|
|Provision user                                     |
|Enrollee information updated, update user          |
|No change to enrollee information, do nothing      | 


|Possible values for `accessType` output        |
|-----------------------------------------------|
|Independent User – with OBOs, Pharmacy         |
|Independent User – with OBOs                   |
|Independent User – without OBOs                |
|On-behalf-of User                              |
|On-behalf-of User – Pharmacy                   |
|Device Provider Agent – with OBOs              |
|On-behalf-of User – Device Provider            |
|On-behalf-of User (can prescribe independently)|

