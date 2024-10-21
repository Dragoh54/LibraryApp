## Starting Postgresql server
```powershell
cd LibraryApp.Api
docker-compose -f .\docker-compose.yml up
```

## Setting the configs
```powershel
$jwt_secret_key = "Your Jwt secret key"
dotnet user-secrets set "ConnectionStrings:LibraryAppDbContext" "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=library_db;"
dotnet user-secrets set "JWTSecretKey" "$jwt_secret_key"
```