#TODO:
- configure delete behaviour for entities
- 



dotnet ef migrations add InitialDbCreation --project DAL.App.EF --startup-project WebApp
dotnet ef database update --project DAL.App.EF --startup-project WebApp
dotnet ef database drop --project DAL.App.EF --startup-project WebApp

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

dotnet aspnet-codegenerator controller -name AddressesController -actions -m Address -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CategoriesController -actions -m Category -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CustomersController -actions -m Customer -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemsController -actions -m Item -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemChoicesController -actions -m ItemChoice -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemOptionsController -actions -m ItemOption -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemTypesController -actions -m ItemType -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name NutritionInfosController -actions -m NutritionInfo -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OrdersController -actions -m Order -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OrderItemChoicesController -actions -m OrderItemChoice -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OrderRowsController -actions -m OrderRow -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RestaurantsController -actions -m Restaurant -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RestaurantCategoriesController -actions -m RestaurantCategory -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name WorkingHoursesController -actions -m WorkingHours -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f