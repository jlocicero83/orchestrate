using Microsoft.Extensions.Options;
using Orchestrate.Common.ApiClients.DanielsOrchestralApi;
using Orchestrate.Common.Settings;
using Orchestrate.Server.Features.DanielsDatabase.FetchWorkDetail;
using Orchestrate.Server.Features.DanielsDatabase.SearchDatabase;

namespace Orchestrate.Server.Features.DanielsDatabase
{
  public static class DanielsDatabaseDependencyResolution
  {
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
      services.AddHttpClient<IDanielsOrchestralApiClient, DanielsOrchestralApiClient>((provider, client) =>
      {
        var options = provider.GetRequiredService<IOptions<DanielsApiSettings>>().Value;
        client.BaseAddress = new Uri(options.BaseUrl);
      });

      // Search Db
      services.AddScoped<ISearchDatabaseRequestHandler, SearchDatabaseRequestHandler>();
      services.AddSingleton<IDanielsV3ToComposerWorkModelMapper, DanielsV3ToComposerWorkModelMapper>();

      // Fetch Work
      services.AddScoped<IFetchWorkDetailRequestHandler, FetchWorkDetailRequestHandler>();
      services.AddSingleton<IDanielsV3ToFullWorkDetailModelMapper, DanielsV3ToFullWorkDetailModelMapper>();
    }
  }
}
