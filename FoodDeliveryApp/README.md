

Secure controllers (edit, index, details and etc) Make sure that user can't see some data and can't change some data
User management UI ??
User ---< Restaurants ??

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

dotnet ef migrations add InitialDbCreation --project DAL.App.EF --startup-project WebApp
dotnet ef database update --project DAL.App.EF --startup-project WebApp
dotnet ef database drop --project DAL.App.EF --startup-project WebApp

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
dotnet aspnet-codegenerator controller -name AddressesController -actions -m Address -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CategoriesController -actions -m Category -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CustomersController -actions -m Customer -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemsController -actions -m Item -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemChoicesController -actions -m ItemChoice -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemInTypeController -actions -m ItemInType -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemOptionsController -actions -m ItemOption -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemTypesController -actions -m ItemType -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name NutritionInfosController -actions -m NutritionInfo -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OrdersController -actions -m Order -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OrderItemChoicesController -actions -m OrderItemChoice -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OrderRowsController -actions -m OrderRow -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RestaurantUserController -actions -m RestaurantUser -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RestaurantCategoriesController -actions -m RestaurantCategory -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name WorkingHoursesController -actions -m WorkingHours -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

dotnet aspnet-codegenerator controller -name AddressesController -actions -m Address -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name CategoriesController -actions -m Category -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name CustomersController -actions -m Customer -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ItemsController -actions -m Item -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ItemChoicesController -actions -m ItemChoice -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ItemInTypeController -actions -m ItemInType -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ItemOptionsController -actions -m ItemOption -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ItemTypesController -actions -m ItemType -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name NutritionInfosController -actions -m NutritionInfo -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name OrdersController -actions -m Order -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name OrderItemChoicesController -actions -m OrderItemChoice -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name OrderRowsController -actions -m OrderRow -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name RestaurantsController -actions -m Restaurant -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name RestaurantCategoriesController -actions -m RestaurantCategory -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name WorkingHoursesController -actions -m WorkingHours -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
!!!BE CAREFUL!!! May introduce new database context to your project if your context is specified incorrectly
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
dotnet aspnet-codegenerator identity -dc DAL.App.EF.AppDbContext -f
 
 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


Shared, common codebase
Contracts.DAL.Base - specs for domain metadata and PK in entities. Specs for common base repository.
DAL.Base - abstract implementations of interfaces for domain.
DAL.Base.EF - implementation of common base repository done in EF.

App specific codebase for our solution
Domain - Domain objects - what is our business about
Contracts.DAL.App - specs for repositories
DAL.App.EF - implementation of repositories