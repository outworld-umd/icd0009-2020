### Useful commands
##### To generate database:
```
dotnet ef database drop -f --project DAL.App.EF --startup-project WebApp --context DAL.App.EF.AppDbContext
rd /q /s DAL.App.EF\Migrations
dotnet ef migrations add InitialDbCreation --project DAL.App.EF --startup-project WebApp --context DAL.App.EF.AppDbContext
dotnet ef database update --project DAL.App.EF --startup-project WebApp --context DAL.App.EF.AppDbContext
```
### Generating API controllers
```
dotnet aspnet-codegenerator controller -name QuizzesController -actions -m Domain.App.Quiz -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name QuestionsController -actions -m Domain.App.Question -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ChoicesController -actions -m Domain.App.Choice -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name AnswersController -actions -m Domain.App.Answer -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name QuizSessionsController -actions -m Domain.App.QuizSession -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
```
### Generating MVC/Views controllers
```
dotnet aspnet-codegenerator controller -name QuizzesController -actions -m Domain.App.Quiz -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name QuestionsController -actions -m Domain.App.Question -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ChoicesController -actions -m Domain.App.Choice -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name AnswersController -actions -m Domain.App.Answer -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name QuizSessionsController -actions -m Domain.App.QuizSession -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
```
After scaffolding do global search & replace:
~~~
</dd class> => </dd>
asp-validation-summary="ModelOnly" => asp-validation-summary="All"
~~~ 