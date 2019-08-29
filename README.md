# optimize-prime

docker-compose up --build to run

prime-dotnet-webapi:
- dotnet ef migrations add InitialCreate
- dotnet ef database update