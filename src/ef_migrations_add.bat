dotnet ef migrations add IdentityInit -c ApplicationDbContext ^
-p BudgetTracker.Infrastructure\BudgetTracker.Infrastructure.csproj ^
-s BudgetTracker.WebApi\BudgetTracker.WebApi.csproj ^
-o Identity\Migrations

dotnet ef migrations add Init -c BudgetTrackerDbContext ^
-p BudgetTracker.Infrastructure\BudgetTracker.Infrastructure.csproj ^
-s BudgetTracker.WebApi\BudgetTracker.WebApi.csproj ^
-o Data\Migrations