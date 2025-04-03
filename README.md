# Gutty System Backend

## Databse MIgraition Creation

```bash
dotnet ef migrations add InitialCreate --project .\src\Infrastructure\ --startup-project .\src\Presentation\
```
## Database Update

```bash
dotnet ef database update --project .\src\Infrastructure\ --startup-project .\src\Presentation\
```

## Running the Application

```bash
dotnet run --project .\src\Presentation\
```

## Seeding the Database
### Seeding Roles
```bash
dotnet run --project .\src\Presentation\ seedRoles
```
