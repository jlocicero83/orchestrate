using Orchestrate.Common.ApiClients.DanielsOrchestralApi;

namespace Orchestrate.Server.Features.DanielsDatabase.FetchWorkDetail
{
  public interface IFetchWorkDetailRequestHandler
  {
    Task<FullWorkDetail> FetchWorkAsync(FetchWorkDetailRequest request, CancellationToken token);
  }
  public class FetchWorkDetailRequestHandler(
    IDanielsOrchestralApiClient apiClient,
    IDanielsV3ToFullWorkDetailModelMapper mapper) : IFetchWorkDetailRequestHandler
  {
    private readonly IDanielsOrchestralApiClient _apiClient = apiClient;
    private readonly IDanielsV3ToFullWorkDetailModelMapper _mapper = mapper;

    public async Task<FullWorkDetail> FetchWorkAsync(FetchWorkDetailRequest request, CancellationToken token)
    {
      var response = await _apiClient.FetchWorkAsync(request.WorkId, token);

      return _mapper.Map(response);
    }
  }
}
