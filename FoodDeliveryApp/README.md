# Food Delivery App
## TODOs
### Layered Architecture 101
**BLL.App/Mappers** -> think about custom mappers
**WebApp** -> make controllers use bll
### Other stuff
* Internationalization
* Make API Controllers use repos
## Useful Commands
##### To generate database:
```
dotnet ef database drop -f --project DAL.App.EF --startup-project WebApp
rd /q /s DAL.App.EF\Migrations
dotnet ef migrations add InitialDbCreation --project DAL.App.EF --startup-project WebApp
dotnet ef database update --project DAL.App.EF --startup-project WebApp
```
##### To create ASP.NET controller and view:
```
dotnet aspnet-codegenerator controller -name {entity}Controller -actions -m {entity} -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
```
##### To create API controller:
```
dotnet aspnet-codegenerator controller -name {entity}Controller -actions -m {entity} -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f

```
##### To generate Identity UI:
```
dotnet aspnet-codegenerator identity -dc DAL.App.EF.AppDbContext -f
```

## Project structure
**Contracts.DAL.App** - specs for repositories

**Contracts.DAL.Base** - specs for domain metadata and PK in entities. Specs for common base repository.

**DAL.App.EF** - implementation of repositories

**DAL.Base** - abstract implementations of interfaces for domain.

**DAL.Base.EF** - implementation of common base repository done in EF.

**Extensions** - different extensions for project

**Domain** - Domain objects - what is our business about


