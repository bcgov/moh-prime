#!/bin/bash
export MAILSERVER=`route -n|grep "UG"|grep -v "UGH"|cut -f 10 -d " "`
dotnet prime.dll
