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
Information will be returned if and only if all four parameters match data in the PRIME database, and the enrollee
has indicated consent to share their information for the given `CareSetting` and API `client_id`.

The response will contain the following information for an enrollee that has signed a Terms of Access (TOA) agreement.  E.g.
```
{
    "result": [
        {
            "gpid": "H86$J0C3Z$6DYHDFUZ@N",
            "status": "Complete",
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

If a BCSC user has not submitted the enrollment yet or never enrolled, or for enrollees that are Under Review or that haven't signed a TOA (Requires TOA) nothing is returned:
```
{
    "result": []
}
```

In the case of an indefinite absence (absence From date provided but no To date given), the status will be `Indefinite absence` and the vendor should deprovision this user, e.g.
```
{
    "result": [
        {
            "gpid": "H86$J0C3Z$6DYHDFUZ@N",
            "status": "Indefinite absence",
            "accessType": "",
            "licences": [
            ]
        }
    ]
}
```

For enrollees that have their renewal period expired and have not renewed, they will have a `status` of `Past Renewal`.
```
{
    "result": [
        {
            "gpid": "H86$J0C3Z$6DYHDFUZ@N",
            "status": "Past Renewal",
            "accessType": null,
            "licences": null
        }
    ]
}
```

For enrollees that have been `locked` by PRIME administrators (such that they cannot view or edit their enrollment details, even if
previously approved), the API response will be:
```
{
    "result": [
        {
            "gpid": "H86$J0C3Z$6DYHDFUZ@N",
            "status": null,
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
            "status": "Complete",
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

|Possible values for `status`|
|----------------------------|
|Complete|
|Indefinite absence|
|Past Renewal|

|Possible values for `accessType`|
|--------------------------------|
|Independent User – with OBOs, Pharmacy|
|Independent User - with OBOs|
|Independent User - without OBOs|
|On-behalf-of User|
|On-behalf-of User – Pharmacy|
