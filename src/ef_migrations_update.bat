dotnet ef database update -c ApplicationDbContext ^
-p BudgetTracker.Infrastructure\BudgetTracker.Infrastructure.csproj ^
-s BudgetTracker.WebApi\BudgetTracker.WebApi.csproj

dotnet ef database update -c BudgetTrackerDbContext ^
-p BudgetTracker.Infrastructure\BudgetTracker.Infrastructure.csproj ^
-s BudgetTracker.WebApi\BudgetTracker.WebApi.csproj