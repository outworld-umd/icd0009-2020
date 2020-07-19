# Food Delivery App
## TODOs
### Layered Architecture 101
* **BLL.App/Mappers** -> think about custom mappers
* **WebApp** -> make controllers use bll
### Other stuff
* Internationalization
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

dotnet aspnet-codegenerator controller -name AddressController -actions -m Address -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CategoryController -actions -m Category -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemController -actions -m Item -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemChoiceController -actions -m ItemChoice -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemInTypeController -actions -m ItemInType -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemOptionController -actions -m ItemOption -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemTypeController -actions -m ItemType -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name NutritionInfoController -actions -m NutritionInfo -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OrderController -actions -m Order -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OrderItemChoiceController -actions -m OrderItemChoice -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OrderRowController -actions -m OrderRow -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RestaurantController -actions -m Restaurant -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RestaurantCategoryController -actions -m RestaurantCategory -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RestaurantUserController -actions -m RestaurantUser -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name WorkingHoursController -actions -m WorkingHours -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
```
##### To create API controller:
```
dotnet aspnet-codegenerator controller -name {entity}Controller -actions -m {entity} -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f

dotnet aspnet-codegenerator controller -name AddressController -actions -m Address -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name CategoryController -actions -m Category -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ItemController -actions -m Item -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ItemChoiceController -actions -m ItemChoice -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ItemInTypeController -actions -m ItemInType -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ItemOptionController -actions -m ItemOption -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ItemTypeController -actions -m ItemType -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name NutritionInfoController -actions -m NutritionInfo -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name OrderController -actions -m Order -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name OrderItemChoiceController -actions -m OrderItemChoice -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name OrderRowController -actions -m OrderRow -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name RestaurantController -actions -m Restaurant -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name RestaurantCategoryController -actions -m RestaurantCategory -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name RestaurantUserController -actions -m RestaurantUser -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name WorkingHoursController -actions -m WorkingHours -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
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


