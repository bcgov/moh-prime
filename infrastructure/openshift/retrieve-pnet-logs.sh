#!/usr/bin/env bash

# Exit immediately if a command exits with a non-zero status.
set -e

main() {

  echo -e "-------- STARTING CRON --------\n"

  openssl help

  psql -V

  echo -e "PGHOST:  ${PGHOST}"

  ls -l /opt/certs

  # ls -l /opt/app-root/etc/certs
}

main  # Ensure the whole file is downloaded before executing
