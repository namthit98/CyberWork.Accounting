# CMD

- Setup environtment: docker compose up
- Create Migrations: dotnet ef migrations add "Init" -p src/Infrastructure -s src/API -o Persistence/Migrations
- Run : cd src/API && dotnet run

# Setup environment
- dotnet user-secrets init
- dotnet user-secrets set "ConnectionStrings:DefaultConnection" "12345"
- dotnet user-secrets list

Ref: https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=linux
