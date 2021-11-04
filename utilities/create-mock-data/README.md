# How to run

`dotnet run <number of Enrollees to generate> <number of Sites to generate>`.  Zero is an acceptable parameter.  The target database to generate the Enrollees into is stored in the `appsettings.json` file.  Change as desired.  


# ModelFactories and Lookups

Many ModelFactories and Lookups files are copied verbatim from `prime-dotnet-webapi-tests`.  The ModelFactories (`Faker` subclasses) have the `SetBaseRules` call disabled.  Other changes as necessary include:

1. `RuleFor` re: the `Id` of the object is disabled since we let the database assign the `Id`.  For `EnrolleeFactory` we do not disable assigning the `Id` 
because of some foreign key relationships that need to be established.

2. For `EnrolleeFactory`, set `IdCounter` to a high enough value to not conflict with any existing `Enrollee` rows in the target database (`select max(e."Id")
from "Enrollee" e`).  Creating AdjudicatorNotes and EnrolleeAbsences is disabled at the moment due to database referential integrity issues; more investigation needed.


# Trouble-shooting

In case of errors such as:
```
  Exception data:
    Severity: ERROR
    SqlState: 23505
    MessageText: duplicate key value violates unique constraint "PK_VendorLookup"
    Detail: Key ("Code")=(8) already exists.
    SchemaName: public
    TableName: VendorLookup
    ConstraintName: PK_VendorLookup
    File: nbtinsert.c
    Line: 434
    Routine: _bt_check_unique
```
Where EF Core gets confused and is trying to insert into a lookup table, check whether the code rather than the object is being set.  
For example, in this case, `SiteVendor.VendorCode` should be set in the factory, rather than `SiteVendor.Vendor`, to avoid the error.

