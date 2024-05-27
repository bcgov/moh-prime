# HpdidLookup API

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


## Step 2:  Call HpdidLookup API with one or more HPDID values

An HPDID is associated with an enrollee's BCSC in a 1-to-1 manner.  In the call to this API, pass along the interested HPDIDs (for multiple PRIME enrollees) from the calling system, the `careSetting` value (see Appendix for possible values), as well as the token obtained in the previous step, e.g.:

```
curl --location --request GET 'https://dev.pharmanetenrolment.gov.bc.ca/api/v1/provisioner-access/gpids?hpdids=gtcochh2vajdtodkby27kspv554dn4is&hpdids=kax2r4lbr2ejsew4ba5bivvsk5onfqaj&careSetting=NHA' \
--header 'Authorization: Bearer eyTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT'
```

### About the parameters

Values for `hpdids` should only be for the users of the system permitting them to access PharmaNet (???).  Whether or not a user may potentially work at another care setting, for privacy and security reasons, the `hpdids` can only be sourced from the data of that installation of software.  

The `careSetting` parameter should be set to the Health Authority code where the software used by potential PRIME enrollees is installed.  For example, in the special case of CareConnect software, if it is installed in a Health Authority care setting, the code of that Health Authority should be passed.  However if it is installed in a PCHP care setting, `CC` should be passed.  

Also there is a limit to the number of HPDIDs accepted in a single call:  10 (subject to change depending on performance testing results).  If too many HPDIDs are provided, a HTTP status code of 400 (Bad Request) is returned.

### API Response scenarios

The response will contain the GPID associated with each enrollee that has signed a Terms of Access (TOA) agreement, when the given `careSetting` value matches the care setting of the enrollee as known in PRIME.  Example response:
```
{
    "result": [
        {
            "hpdid": "kax2r4lbr2ejsew4ba5bivvsk5onfqaj",
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

> **Note:**
> `renewalDate` will no longer be provided in the response.


If a BCSC user has not submitted the enrollment yet or never enrolled, nothing is returned:
```
{
    "result": []
}
```

If the given `careSetting` value does not match any of the enrollee's care settings in PRIME, the API response will be:
```
{
    "result": [
        {
            "hpdid": "kax2r4lbr2ejsew4ba5bivvsk5onfqaj",
            "gpid": null,
            "status": "CareSetting mismatch",
            "accessType": null,
            "licences": null
        }
    ]
}
```
Note that `CC` will match a Private Community Health Practice (PCHP) care setting.

For enrollees that have been `locked` by PRIME administrators (such that they cannot view or edit their enrollment details, even if
previously approved), the API response will be:
```
{
    "result": [
        {
            "hpdid": "kax2r4lbr2ejsew4ba5bivvsk5onfqaj",
            "gpid": null,
            "status": null,
            "accessType": "",
            "licences": [
            ]
        }
    ]
}
```

Enrollees that are Under Review or that haven't signed a TOA (Requires TOA) have a `status` of `Incomplete`, e.g.
```
{
    "result": [
        {
            "hpdid": "kax2r4lbr2ejsew4ba5bivvsk5onfqaj",
            "gpid": null,
            "status": "Incomplete",
            "accessType": null,
            "licences": null
        }
    ]
}
```

In the case of an indefinite absence (absence From date provided, starting today or in the past, but no To date given), the status will be `Indefinite absence` and the vendor should deprovision this user, e.g.
```
{
    "result": [
        {
            "hpdid": "kax2r4lbr2ejsew4ba5bivvsk5onfqaj",
            "gpid": "H86$J0C3Z$6DYHDFUZ@N",
            "status": "Indefinite absence",
            "accessType": null,
            "licences": null
        }
    ]
}
```

For enrollees that have their renewal period expired and have not renewed, they will have a `status` of `Past Renewal`.
```
{
    "result": [
        {
            "hpdid": "kax2r4lbr2ejsew4ba5bivvsk5onfqaj",
            "gpid": "H86$J0C3Z$6DYHDFUZ@N",
            "status": "Past Renewal",
            "accessType": null,
            "licences": null
        }
    ]
}
```
In the case the enrollee is past their renewal period and has also reported an indefinite absence, PRIME will return a status of `Indefinite absence`.

Lastly, due to privacy issues, in the very rare cases that a PRIME enrollee has more than one licence, for each licence, the licence-related information would be blanked-out and a licence-level Boolean field `redacted` would be set to `true`, e.g.
```
{
    "result": [
        {
            "hpdid": "kax2r4lbr2ejsew4ba5bivvsk5onfqaj",
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

|Possible value for `careSetting` input parameter|Human-readable definition|
|------------------------------------------------|-------------------------|
|NHA                                             |Northern Health Authority|
|IHA                                             |Interior Health Authority|
|VCHA                                            |Vancouver Coastal Health Authority|
|VIHA                                            |Vancouver Island Health Authority|
|FHA                                             |Fraser Health Authority|
|PHSA                                            |Provincial Health Services Authority|
|CC                                              |CareConnect|


### PRIME API behavior/`status` output 

The PRIME API returns enrollee data only under certain conditions.  Data is not returned if the person is not in the PRIME system, if the API client should not receive the data (`CareSetting mismatch`), or if PRIME administrators do not want the data shared with any external systems.  Then if the enrollee has not fully completed their enrollment, some or most data is also withheld.  Finally if all conditions are met, enrollee data is returned to the API client.

|Possible values for `status` output|Conditions for `status`|
|-----------------------------------|-----------------------|
|*no details whatsoever*            |BCSC user has not submitted the enrollment yet or never enrolled| 
|CareSetting mismatch               |Provided careSetting value does not match care setting of enrollee|
|*no status or other details*       |Enrollee has been `locked` by PRIME administrators|
|Incomplete                         |Under Review or enrollee hasn't signed TOA|
|Indefinite absence                 |TOA signed but reporting indefinite absence|
|Past Renewal                       |TOA signed but agreement expired|
|Complete                           |TOA signed and none of other conditions applicable| 


|Possible values for `accessType` output|
|--------------------------------|
|Independent User – with OBOs, Pharmacy|
|Independent User – with OBOs|
|Independent User – without OBOs|
|On-behalf-of User|
|On-behalf-of User – Pharmacy|
|Device Provider Agent – with OBOs|
|On-behalf-of User – Device Provider|
|On-behalf-of User (can prescribe independently)|
