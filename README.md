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
### Seeding All
```bash
dotnet run --project .\src\Presentation\ seed
```

### Seeding Roles
```bash
dotnet run --project .\src\Presentation\ seedRoles
```


### Seeding Admin User
```bash
dotnet run --project .\src\Presentation\ seedAdmin
```
#### Admin User
- Phone: 1234567890
- Password: 1234567890
