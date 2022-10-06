# HpdidLookup API

## Step 1:  Obtain JWT Bearer Token

Connect to Keycloak instance associated with the PRIME environment, passing credentials assigned to your system.  E.g.

```
curl --location --request POST 'https://dev.oidc.gov.bc.ca/auth/realms/v4mbqqas/protocol/openid-connect/token' \
--header 'Content-Type: application/x-www-form-urlencoded' \
--data-urlencode 'grant_type=client_credentials' \
--data-urlencode 'client_id=IDIDIDIDIDIDIDIDIDID' \
--data-urlencode 'client_secret=SSSSSSSSSSSSSSSSSSSS'
```

The response will be something like this, with the token embedded:

```
{
    "access_token": "eyTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT",
    "expires_in": 36000,
    "refresh_expires_in": 0,
    "token_type": "bearer",
    "not-before-policy": 1571785607,
    "scope": ""
}
```


## Step 2:  Call HpdidLookup API with one or more HPDID values

An HPDID is associated with an enrollee's BCSC in a 1-to-1 manner.  In the call to this API, pass along the interested HPDIDs (for multiple PRIME enrollees) as well as the token obtained in the previous step, e.g.:

```
curl --location --request GET 'https://dev.pharmanetenrolment.gov.bc.ca/api/v1/provisioner-access/gpids?hpdids=gtcochh2vajdtodkby27kspv554dn4is&hpdids=kax2r4lbr2ejsew4ba5bivvsk5onfqaj' \
--header 'Authorization: Bearer eyTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT'
```

There is a limit to the number of HPDIDs accepted in a single call:  10 (subject to change depending on performance testing results).  If too many HPDIDs are provided, a HTTP status code of 400 (Bad Request) is returned.

The response will contain the GPID associated with each enrollee that has signed a Terms of Access (TOA) agreement.  Details of enrollees that haven't signed a TOA are filtered out from the API's response.  E.g.

```
{
    "result": [
        {
            "hpdid": "kax2r4lbr2ejsew4ba5bivvsk5onfqaj",
            "gpid": "H86$J0C3Z$6DYHDFUZ@N",
            "renewalDate": "2023-08-03T14:35:09.670054-06:00",
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

Due to privacy issues, in the very rare cases that a PRIME enrollee has more than one licence, for each licence, the licence-related information would be blanked-out and a licence-level Boolean field `redacted` would be set to `true`, e.g.

```
{
    "result": [
        {
            "hpdid": "kax2r4lbr2ejsew4ba5bivvsk5onfqaj",
            "gpid": "H86$J0C3Z$6DYHDFUZ@N",
            "renewalDate": "2023-08-03T14:35:09.670054-06:00",
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
