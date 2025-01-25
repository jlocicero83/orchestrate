using Orchestrate.Common.ApiClients.DanielsOrchestralApi;
using Orchestrate.Common.Settings;
using Orchestrate.Server.Context;
using Orchestrate.Server.Features.DanielsDatabase;
using Orchestrate.Server.Services.TenantResolver;

namespace Orchestrate.Server.Infrastructure
{
  public static class DependencyResolution
  {
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
      services.RegisterSettings(configuration);
      services.RegisterServices(configuration);
      services.RegisterFeatures(configuration);
      services.RegisterApis(configuration);
    }

    private static void RegisterSettings(this IServiceCollection services, IConfiguration configuration)
    {
      services.Configure<DanielsApiSettings>(configuration.GetSection("DanielsApiSettings"));
    }

    private static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddScoped<ITenantResolverService, TenantResolverService>();
      services.AddScoped<TenantContext>();
    }

    private static void RegisterFeatures(this IServiceCollection services, IConfiguration configuration)
    {
      // Daniel's Database
      DanielsDatabaseDependencyResolution.Configure(services, configuration);

    }

    private static void RegisterApis(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddScoped<IDanielsOrchestralApiClient, DanielsOrchestralApiClient>();
    }
  }
}
