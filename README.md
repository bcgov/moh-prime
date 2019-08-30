# optimize-prime

docker-compose up --build to run

The web front end can be accessed through the root URL it is deployed
at, e.g. localhost for a local deployment.

Accessing the database can be done by creating a connection to port 
5432 using a database tool such as DBeaver.



prime-dotnet-webapi:
- dotnet ef migrations add InitialCreate
- dotnet ef database update


APACHE 2.0 LICENSE

Copyright 2019 Sierra Systems Group Inc.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.