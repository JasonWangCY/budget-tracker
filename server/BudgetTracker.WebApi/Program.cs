using Serilog;
using BudgetTracker.WebApi.Configs;
using BudgetTracker.Application.Utils;
using BudgetTracker.WebApi.Middleware;

SetupConfigs.SetUpLogger();
var app = ConfigureBuilder().Build();
if (EnvironmentManager.GetSeedDb())
{
    await SetupConfigs.SeedDatabase(app);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

WebApplicationBuilder ConfigureBuilder()
{
    var builder = WebApplication.CreateBuilder(args);
    var conf = builder.Configuration;

    builder.Services.RegisterServices()
        .RegisterDatabase()
        .RegisterAuth(conf)
        .ConfigApi();

    Log.Information("App created...");
    return builder;
}

