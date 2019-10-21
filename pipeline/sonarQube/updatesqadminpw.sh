#!/usr/bin/env bash

#generate new admin password to override silly default
admin_password=$(oc -o json get secret sonarqube-admin-password | sed -n 's/.*"password": "\(.*\)",/\1/p' | base64 --decode)

echo "Generated password is '$admin_password'. Enter to continue..."
read BLAH

# figure out the sonarqube route so we know where to access the API
sonarqube_url=$(oc get routes | awk '/sonarqube/{print $2}')
curl -G -X POST -v -u admin:admin --data-urlencode "login=admin" --data-urlencode "password=$admin_password" --data-urlencode "previousPassword=admin" "https://$sonarqube_url/api/users/change_password"
