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

Example of SecretKey: `A1b#c9_D3e*f7!G4h/i2j-K8l+m%0N6o?P3r$T2v&W9x*U1y#Z4q`

## First run
```poweshell
dotnet run --seed
```
