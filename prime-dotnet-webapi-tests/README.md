# How to test

To run automated tests:  `dotnet test`

To show which tests ran and with what data:  `dotnet test -v n`


# To test CHES Client

Using Windows PowerShell:

1. `setx CHES_ENABLED "true"`
2. You may need to restart VS Code so that it is aware of new environment variable
3. `Get-ChildItem env:` to check that `CHES_ENABLED` is set
4. `dotnet bin/Debug/netcoreapp3.1/prime.dll`
5. Perform tests, understanding that emails will go to intended email addresses when using the Common Hosted Email Service, rather than MailHog
6. When done testing CHES Client, `setx CHES_ENABLED "false"`.  Restart VS Code and double-check environment is updated
