#!/bin/bash

# add this into the env in yml file for the backup container
KEYCLOAK_REALM="moh_applications" # Replace with your auth URL
KEYCLOAK_URL="https://common-logon-test.hlth.gov.bc.ca/auth"
KEYCLOAK_CLIENT_ID="PRIME-APPLICATION-SERVICE-ACCOUNT"
KEYCLOAK_CLIENT_SECRET="ForSHMIC3gJlWFUW5CRGtqkAexLjDJfT"

ARCHIVE_DATE_RANGE_URL="http://test-webapi:8080/api/jobs/archive/transaction-log"
ARCHIVE_CLEAR_URL="http://test-webapi:8080/api/jobs/archive/transaction-log"


# Function to get the API token
get_api_token() {

    local TOKEN_RESPONSE=$(curl -X POST "$KEYCLOAK_URL/realms/$KEYCLOAK_REALM/protocol/openid-connect/token" \
        -H "Content-Type: application/x-www-form-urlencoded" \
        -d "grant_type=client_credentials" \
        -d "client_id=$KEYCLOAK_CLIENT_ID" \
        -d "client_secret=$KEYCLOAK_CLIENT_SECRET")

    # Check for errors in the response
    if grep -q "error" <<< "$TOKEN_RESPONSE"; then
        echo "Error obtaining token: $TOKEN_RESPONSE"
        exit 1
    fi

    ACCESS_TOKEN=$(echo "$TOKEN_RESPONSE" | sed 's/.*access_token":"\([^"]*\)".*/\1/')

    if [ -z "$ACCESS_TOKEN" ]; then
        echo "Failed to parse access token."
        exit 1
    fi

    echo "$ACCESS_TOKEN"
}

# Function to call the second API using the retrieved token
get_transaction_log_date_range() {

    local TOKEN=$1 # Accept the token as an argument

    # Use the token in the Authorization header (assuming Bearer token)
    local RESPONSE=$(curl -s -X GET "$ARCHIVE_DATE_RANGE_URL" \
        -H "Authorization: Bearer $TOKEN" \
        -H "Accept: application/json")

    # Check for curl errors or API specific errors in response
    if [ $? -ne 0 ] || [[ "$RESPONSE" == *error* ]]; then
        echo "Error getting string" >&2
        return 1
    fi

    # This pattern matches the value inside the "result_string":"..." field
    RESULT_VALUE=$(echo "$RESPONSE" | grep -o '"result":"[^"]*' | sed 's/"result":"//')

    if [ -z "$RESULT_VALUE" ]; then
      echo "Failed to extract 'result' from API response. Response: $RESPONSE"
      exit 1
    fi
    
    echo "$RESULT_VALUE"

}

clear_transaction_log_archive(){
    local token=$1 # Accept the token as an argument

    # Use the token in the Authorization header (assuming Bearer token)
    local response=$(curl -s -X GET "$ARCHIVE_CLEAR_URL" \
        -H "Authorization: Bearer $token" \
        -H "Content-Length: 0")

    # Echo the string so it can be captured
    echo "Transaction log archive clear response: $response"
}

# Main execution
echo "Starting API calls..."

# 1. Get the token by capturing the output of the function
API_TOKEN=$(get_api_token)


    # Use the token in the Authorization header (assuming Bearer token)
    RESPONSE=$(curl -s -X GET "$ARCHIVE_DATE_RANGE_URL" \
        -H "Authorization: Bearer $API_TOKEN" \
        -H "Accept: application/json")

    # Check for curl errors or API specific errors in response
    if [ $? -ne 0 ] || [[ "$RESPONSE" == *error* ]]; then
        echo "Error getting string" >&2
        # return 1
    fi

    # This pattern matches the value inside the "result_string":"..." field
    RESULT_VALUE=$(echo "$RESPONSE" | grep -o '"result":"[^"]*' | sed 's/"result":"//')

    if [ -z "$RESULT_VALUE" ]; then
      echo "Failed to extract 'result' from API response. Response: $RESPONSE"
      # exit 1
    fi
    
    echo "$RESULT_VALUE"



