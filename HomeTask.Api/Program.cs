using Application;
using HealthChecks.UI.Client;
using HomeTask.Api.Extensions;
using Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Persistence;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services
    .AddApplication()
    .AddPersistence(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.ApplyMigrations();

    app.SeedData();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

namespace HomeTask.Api
{
    public partial class Program;
}