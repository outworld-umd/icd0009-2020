# Food Delivery App
## TODOs
### Partial Views
* Hidden input from _CreateEdit -> Edit.cshtml
### User-Specific Views!
### Securing
* API Controllers
* ASP.NET Controllers
### Other stuff
* Internationalization
## Useful Commands
##### To generate database:
```
dotnet ef database drop -f --project DAL.App.EF --startup-project WebApp --context DAL.App.EF.AppDbContext
rd /q /s DAL.App.EF\Migrations
dotnet ef migrations add InitialDbCreation --project DAL.App.EF --startup-project WebApp --context DAL.App.EF.AppDbContext
dotnet ef database update --project DAL.App.EF --startup-project WebApp --context DAL.App.EF.AppDbContext
```
##### To create ASP.NET controller and view:
```
dotnet aspnet-codegenerator controller -name {entity}Controller -actions -m {entity} -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name AddressController -actions -m Domain.App.Address -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CategoryController -actions -m Domain.App.Category -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemController -actions -m Domain.App.Item -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemChoiceController -actions -m Domain.App.ItemChoice -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemInTypeController -actions -m Domain.App.ItemInType -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemOptionController -actions -m Domain.App.ItemOption -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemTypeController -actions -m Domain.App.ItemType -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name NutritionInfoController -actions -m Domain.App.NutritionInfo -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OrderController -actions -m Order -dc Domain.App.AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OrderItemChoiceController -actions -m Domain.App.OrderItemChoice -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OrderRowController -actions -m Domain.App.OrderRow -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RestaurantController -actions -m Domain.App.Restaurant -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RestaurantCategoryController -actions -m Domain.App.RestaurantCategory -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RestaurantUserController -actions -m Domain.App.RestaurantUser -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name WorkingHoursController -actions -m Domain.App.WorkingHours -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name LangStrController -actions -m Domain.App.LangString -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TranslationController -actions -m Domain.App.Translation -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

```
##### To create API controller:
```
dotnet aspnet-codegenerator controller -name {entity}Controller -actions -m {entity} -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f

dotnet aspnet-codegenerator controller -name AddressController -actions -m Domain.App.Address -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name CategoryController -actions -m Domain.App.Category -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ItemController -actions -m Domain.App.Item -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ItemChoiceController -actions -m Domain.App.ItemChoice -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ItemOptionController -actions -m Domain.App.ItemOption -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ItemTypeController -actions -m Domain.App.ItemType -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name NutritionInfoController -actions -m Domain.App.NutritionInfo -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name OrderController -actions -m Domain.App.Order -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name OrderItemChoiceController -actions -m Domain.App.OrderItemChoice -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name OrderRowController -actions -m Domain.App.OrderRow -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name RestaurantCategoriesController -actions -m Domain.App.RestaurantCategory -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name WorkingHoursController -actions -m Domain.App.WorkingHours -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
```
##### To generate Identity UI:
```
dotnet aspnet-codegenerator identity -dc DAL.App.EF.AppDbContext -f
```

## Project structure
**Contracts.DAL.App** - specs for repositories

**Contracts.Domain** - specs for domain metadata and PK in entities. Specs for common base repository.

**DAL.App.EF** - implementation of repositories

**DAL.Base** - abstract implementations of interfaces for domain.

**DAL.Base.EF** - implementation of common base repository done in EF.

**Extensions** - different extensions for project

**Domain** - Domain objects - what is our business about


