using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Orchestrate.Common.Settings;

namespace Orchestrate.Common.ApiClients.DanielsOrchestralApi
{
  public interface IDanielsOrchestralApiClient
  {
    Task<List<SearchDatabaseResponseV3Model>> SearchDatabaseAsync(string composer, string work);
    Task<FetchWorkResponseV3Model> FetchWorkAsync(string workId);
  }

  public class DanielsOrchestralApiClient : BaseApiClient<DanielsOrchestralApiClient>, IDanielsOrchestralApiClient
  {
    private readonly string _apiId;
    private readonly string _apiToken;
    public DanielsOrchestralApiClient(
      IOptions<DanielsApiSettings> settings,
      HttpClient httpClient,
      ILogger<DanielsOrchestralApiClient> logger)
      : base(httpClient,
          settings.Value.BaseUrl,
          logger)
    {
      _apiId = settings.Value.ApiId.ToString();
      _apiToken = settings.Value.ApiToken;
    }

    public async Task<List<SearchDatabaseResponseV3Model>> SearchDatabaseAsync(string composer, string work)
    {
      // get headers
      var additionalHeaders = new List<KeyValuePair<string, string>>()
      {
        new("composer", composer),
        new("work", work)
      };

      var headers = GetHeaders(additionalHeaders);

      // build the API request
      var request = new ApiRequest<List<SearchDatabaseResponseV3Model>>
      {
        Method = HttpMethod.Get,
        Resource = "search",
        Headers = headers,
      };

      var response = await ExecuteRequestAsync(request);

      return response.Body;
    }

    public async Task<FetchWorkResponseV3Model> FetchWorkAsync(string workId)
    {
      var additionalHeaders = new List<KeyValuePair<string, string>>()
      {
        new("work", workId)
      };

      var headers = GetHeaders(additionalHeaders);

      var request = new ApiRequest<FetchWorkResponseV3Model>
      {
        Method = HttpMethod.Get,
        Resource = "fetch",
        Headers = headers,
      };

      var response = await ExecuteRequestAsync(request);

      return response.Body;
    }

    private Dictionary<string, string> GetHeaders(IEnumerable<KeyValuePair<string, string>>? additionalHeaders = null)
    {
      var headers = new Dictionary<string, string>
      {
        { "accept", "application/json" },
        { "userId", _apiId },
        { "token", _apiToken }
      };

      if (additionalHeaders != null)
      {
        foreach (var header in additionalHeaders)
        {
          if (!string.IsNullOrEmpty(header.Value))
          {
            headers.Add(header.Key, header.Value);
          }
        }
      }

      return headers;
    }
  }
}
