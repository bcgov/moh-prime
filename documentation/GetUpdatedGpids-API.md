# GetUpdatedGpids API

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


## Step 2:  Call GetUpdatedGpids API with one or more HPDID values and when those HPDIDs were last checked for updates

An HPDID is associated with an enrollee's BCSC in a 1-to-1 manner.  In the call to this API, pass along the interested HPDIDs (for multiple PRIME enrollees), when those HPDIDs were last checked for updates, as well as the token obtained in the previous step, e.g.:

```
curl --location --request POST 'https://dev.pharmanetenrolment.gov.bc.ca/api/v1/provisioner-access/updated-gpids' \
--header 'Authorization: Bearer eyTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT' \
--header 'Content-Type: application/x-www-form-urlencoded' \
--data-urlencode 'hpdids=gtcochh2vajdtodkby27kspv554dn4is' \
--data-urlencode 'hpdids=kax2r4lbr2ejsew4ba5bivvsk5onfqaj' \
--data-urlencode 'hpdids=n2glabsqemlhthadcseqfj5m2j6cd33g' \
--data-urlencode 'updatedSince=2021-09-05'
```

There is a limit to the number of HPDIDs accepted in a single call:  1000 (subject to change depending on performance testing results).  If too many HPDIDs are provided, a HTTP status code of 400 (Bad Request) is returned.

The response is the subset of HPDIDs from the given list of HPDIDs that have a Terms of Access agreement Accepted Date since the given date/time (e.g. 2021-09-05).  For example, a subset of 1 HPDID from the original set may meet this criteria  E.g.

```
{
    "result": [
        "kax2r4lbr2ejsew4ba5bivvsk5onfqaj"
    ]
}
```

