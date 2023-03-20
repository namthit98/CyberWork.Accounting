# CMD

- Setup environtment: docker compose up
- Create Migrations: dotnet ef migrations add "Init" -p src/Infrastructure -s src/API -o Persistence/Migrations
- Run : cd src/API && dotnet run
