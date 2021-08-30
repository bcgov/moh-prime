#!/usr/bin/env bash

# Exit immediately if a command exits with a non-zero status.
set -e

main() {

  echo -e "-------- STARTING CRON --------\n"

  echo -e "Realm:  ${KEYCLOAK_REALM}"

  ls -l /opt/app-root/etc/certs
}

main  # Ensure the whole file is downloaded before executing
