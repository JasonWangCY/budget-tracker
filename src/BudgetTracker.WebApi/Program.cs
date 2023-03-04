using Serilog;
using BudgetTracker.WebApi.Configs;

SetupConfigs.SetUpLogger();
var app = ConfigureBuilder().Build();
await SetupConfigs.SeedDatabase(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

WebApplicationBuilder ConfigureBuilder()
{
    var builder = WebApplication.CreateBuilder(args);
    var conf = builder.Configuration;

    builder.Services.RegisterServices(conf)
        .RegisterDatabase(conf)
        .RegisterAuth(conf)
        .ConfigApi();

    Log.Information("App created...");
    return builder;
}

