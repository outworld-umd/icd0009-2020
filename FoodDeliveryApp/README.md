# Food Delivery App
## TODOs
### Layered Architecture 101
1. DONE (**DAL.App.DTO** -> Address, Category, Item etc.)
2. DONE **DAL.App.EF/Repositories** -> change to EFBaseRepository<AppDbContext, Domain.{_entity_}, DAL.App.DTO.{_entity_}>, add to constructor base(dbContext, new BaseDALMapper<Domain.{_entity_}, DTO.{_entity_}>())
3. DONE **Contracts.DAL.App/Repositories** -> change in every class: using ~~Domain~~DAL.App.DTO;
4. DONE **BLL.App.DTO** -> Address, Category, Item etc.
5. DONE **Contracts.BLL.App/Services** -> add interface I{_entity_}Service : BaseEntityService<BLL.App.DTO.{_entity_}>
6. **BLL.App** -> add {entity}Service with right inheritance etc.
7. **Contracts.BLL.App/IAppBLL.cs** -> add properties for service
8. **BLL.App/AppBLL.cs** -> implement them
9. **BLL.App/Mappers** -> later...
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


