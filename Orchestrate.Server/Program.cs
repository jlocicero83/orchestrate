using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Orchestrate.Server.Data;
using Orchestrate.Server.Infrastructure;
using Orchestrate.Server.Infrastructure.Middleware;
using Orchestrate.Server.Services.TenantResolver;

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
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = "https://dev-kwxbfmcbpwd0gdjb.us.auth0.com"; 
    options.Audience = "https://api.orchestrate_dev.local"; // your registered API identifier
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("Orchestrate", policy =>
    {
        policy.WithOrigins("https://localhost:60912")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddScoped<ITenantResolverService, TenantResolverService>();

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


app.UseCors("Orchestrate");

// Microsoft Settings
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Register Middleware
app.UseMiddleware<ResolveTenantMiddleware>();

app.UseMiddleware<ExceptionHandlingMiddleware>();


app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
