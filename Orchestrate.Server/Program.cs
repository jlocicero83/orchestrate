using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Microsoft.EntityFrameworkCore;
using Orchestrate.Server.Data;
using Orchestrate.Server.Infrastructure;
using Orchestrate.Server.Infrastructure.Middleware;

var serializerSettings = new JsonSerializerOptions
{
  DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
  PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
  DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
  PropertyNameCaseInsensitive = true,
  TypeInfoResolver = new DefaultJsonTypeInfoResolver()
};
serializerSettings.Converters.Add(new JsonStringEnumConverter());

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContextFactory<OrchestrateDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("OrchestrateDev"))
  .UseSnakeCaseNamingConvention());

builder.Services.AddDbContextFactory<TenantDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("OrchestrateDev"))
  .UseSnakeCaseNamingConvention());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  options.CustomSchemaIds(type => type.ToString());
  options.IgnoreObsoleteActions();
  options.IgnoreObsoleteProperties();
});

// Dependency Resolution
DependencyResolution.Configure(builder.Services, builder.Configuration);

// Build the Application
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Microsoft Settings
app.UseHttpsRedirection();
app.UseAuthorization();

// Register Middleware
app.UseMiddleware<ResolveTenantMiddleware>();

app.UseMiddleware<ExceptionHandlingMiddleware>();


app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
