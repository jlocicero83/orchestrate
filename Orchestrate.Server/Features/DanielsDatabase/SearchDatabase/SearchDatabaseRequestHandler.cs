using Orchestrate.Common.ApiClients.DanielsOrchestralApi;
using Orchestrate.Common.Exceptions;

namespace Orchestrate.Server.Features.DanielsDatabase.SearchDatabase
{
  public interface ISearchDatabaseRequestHandler
  {
    Task<List<ComposerWork>> SearchDatabaseAsync(SearchDatabaseRequest request, CancellationToken token);
  }

  public class SearchDatabaseRequestHandler(IDanielsOrchestralApiClient apiClient,
    IDanielsV3ToComposerWorkModelMapper mapper) : ISearchDatabaseRequestHandler
  {
    private readonly IDanielsOrchestralApiClient _apiClient = apiClient;
    private readonly IDanielsV3ToComposerWorkModelMapper _mapper = mapper;

    public async Task<List<ComposerWork>> SearchDatabaseAsync(SearchDatabaseRequest request, CancellationToken token)
    {
      if (string.IsNullOrEmpty(request.Composer) && string.IsNullOrEmpty(request.Work))
      {
        var exception = new BadRequestException("Composer or Work query parameter is required for this request.");
        throw exception;
      }

      var response = await apiClient.SearchDatabaseAsync(request.Composer, request.Work, token);

      return _mapper.Map(response);
    }
  }
}
