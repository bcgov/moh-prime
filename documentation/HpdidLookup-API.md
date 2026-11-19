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

Values for `hpdids` should only be for the users of "the system permitting them to access PharmaNet" \[term for this???\].  Whether or not a user may potentially work at another care setting, for privacy and security reasons, the `hpdids` can only be sourced from the data of that installation of software.  

The `careSetting` parameter should be set to the Health Authority code where the software used by potential PRIME enrollees is installed.  For example, in the special case of CareConnect software, if it is installed in a Health Authority care setting, the code of that Health Authority should be passed.  However if it is installed in a PCHP care setting, `CC` should be passed.  

Also there is a limit to the number of HPDIDs accepted in a single call:  10 (subject to change depending on performance testing results).  If too many HPDIDs are provided, a HTTP status code of 400 (Bad Request) is returned.

### API Response scenarios

When appropriate, this API will return information necessary to provision a user for proper PharmaNet access.  In most other situations, the `status` field will include next steps instructions for the software calling the API, for the noted user (identified by the `hpdid` field).        

If a BCSC user has never enrolled, the following is returned:
```
{
    "result": [
        {
            "hpdid": "kax2r4lbr2ejsew4ba5bivvsk5onfqaj",
            "gpid": null,
            "status": "User not found in PRIME, refer user to PRIME",
            "accessType": null,
            "licences": null
        }
    ]
}
```

When provided `careSetting` value does not match care settings of enrollee:
```
{
    "result": [
        {
            "hpdid": "kax2r4lbr2ejsew4ba5bivvsk5onfqaj",
            "gpid": null,
            "status": "Care Setting not selected, refer user to PRIME",
            "accessType": null,
            "licences": null
        }
    ]
}
```
Note that `CC` will match a Private Community Health Practice (PCHP) care setting.

PRIME administrators may not want enrollee information to be returned:
```
{
    "result": [
        {
            "hpdid": "kax2r4lbr2ejsew4ba5bivvsk5onfqaj",
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
            "hpdid": "kax2r4lbr2ejsew4ba5bivvsk5onfqaj",
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
            "hpdid": "kax2r4lbr2ejsew4ba5bivvsk5onfqaj",
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
            "hpdid": "kax2r4lbr2ejsew4ba5bivvsk5onfqaj",
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
            "hpdid": "kax2r4lbr2ejsew4ba5bivvsk5onfqaj",
            "gpid": "H86$J0C3Z$6DYHDFUZ@N",
            "status": "User past renewal, refer user to PRIME",
            "accessType": null,
            "licences": null
        }
    ]
}
```
In the case the enrollee is past their renewal period and has also reported an indefinite absence, PRIME will return a status of `Indefinite absence, deprovision user`. \[confirm???\]


When none of the previously mentioned conditions is the case, the response will contain details to provision each enrollee that has signed a Terms of Access (TOA) agreement, when the given `careSetting` value matches the care setting of the enrollee as known in PRIME.  Example response for a single enrollee:
```
{
    "result": [
        {
            "hpdid": "kax2r4lbr2ejsew4ba5bivvsk5onfqaj",
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

> **Note:**
> `renewalDate` will no longer be provided in the response.

When the PRIME enrollment has changed from the information previously returned to the API client for the enrollee, the response will be similar to:
```
{
    "result": [
        {
            "hpdid": "kax2r4lbr2ejsew4ba5bivvsk5onfqaj",
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
            "hpdid": "kax2r4lbr2ejsew4ba5bivvsk5onfqaj",
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
            "hpdid": "kax2r4lbr2ejsew4ba5bivvsk5onfqaj",
            "gpid": "H86$J0C3Z$6DYHDFUZ@N",
            "status": "\[what to output here???\]",
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

### About limits to using the data in the API Response

The data should only be used for the purpose of provisioning the PRIME enrollee with the given HPDID into the local software system.  The data should not be shared with another system that permits access to PharmaNet (System B) even if the user may use that other system (System B) in a different care setting.  That is, each system (System A and System B, e.g.) should call this API only for the PRIME enrollees stored in its individual database.  


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

The PRIME API returns enrollee data only under certain conditions.  Data is not returned if the person is not in the PRIME system, if the API client should not receive the data (a care setting mismatch), or if PRIME administrators do not want the data shared with any external systems.  Then if the enrollee has not fully completed their enrollment, some or most data is also withheld.  Finally if all conditions are met, enrollee data is returned to the API client.

|Possible values for `status` output                |
|---------------------------------------------------|
|User not found in PRIME, refer user to PRIME       | 
|Care Setting not selected, refer user to PRIME     |
|None                                               |
|Incomplete enrollment, refer user to PRIME         |
|User past renewal, refer user to PRIME             | 
|Indefinite absence, deprovision user \[higher rank???\]              |
|Enrollee is in defined absence period, deactivate user.  Call again the day after &lt;absence end date&gt; \[higher rank???\]|
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
