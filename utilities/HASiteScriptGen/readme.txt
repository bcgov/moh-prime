*This program only generates the script to insert site data. It assumes the required Care Type, Vendor, HA Care Type, HA Vendor, HA contacts, HA Care Type Vendor and HA Tech Support already in database.

*Steps to generate the SQL insert script:
1) Use Sample-Site-list.xlsx as template and fill the columns
    a) Authorized User Id - AuthorizedUser.Id
    b) Org Id - HealthAuthorityOrganization.Id 
    c) Care Setting - set to 1, which is Health Authority
    d) CareType - name already in HealthAuthoritCareType table
    e) Vendor - name already in VendorLookup table and linked to HealthAuthorityVendor table
    f) Site Mnemonic - put "na" if empty
    g) Name of Site - must have value
    h) SiteId/Pec Co - Site ID, or put "na" if empty
    i) Address Line 1 - must have value
    j) Address Line 2 - put "na" if empty
    k) City - must have value
    l) Province - set to "BC"
    m) Country - set to "Canada"
    n) Postal Code - must have value
    o) Hours of Operation - for 24 hours, put "00:00-24:00", leave it empty if closed
    p) Pharmanet administrator - email address that used in Pharmanet Admin contact for the HA
    q) Technical Support Contact - email address that used in Tech Support contact for the HA
2) Ensure the spreadsheet name is "Sites" in the excel 
3) Open Program.cs file and update some variables,
    i) update the filename variable with the input filename 
    ii) update the outputfilename variable with the output filename
4) Open a terminal and run "dotnet run" to generate SQL script
5) Please try the script in local database, then Test database before pushing to Prod database. 
