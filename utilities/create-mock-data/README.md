# How to run

`dotnet run <number of Enrollees to generate> <number of Sites to generate>`

The target database to generate the Enrollees into is stored in the `appsettings.json` file.  Change as desired.  


# ModelFactories and Lookups

Many ModelFactories and Lookups files are copied verbatim from `prime-dotnet-webapi-tests`.  The ModelFactories (`Faker` subclasses) have the `SetBaseRules` call disabled.  Other changes as necessary include:

1. `RuleFor` re: the `Id` of the object is disabled since we let the database assign the `Id`.  For `EnrolleeFactory` we do not disable assigning the `Id` 
because of some foreign key relationships that need to be established.

2. For `EnrolleeFactory`, set `IdCounter` to a high enough value to not conflict with any existing `Enrollee` rows in the target database (`select max(e."Id")
from "Enrollee" e`).  Creating AdjudicatorNotes and EnrolleeAbsences is disabled at the moment due to database referential integrity issues; more investigation needed.
