using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Orchestrate.Common.Extensions;

namespace Orchestrate.Common.ApiClients
{
  public abstract class BaseApiClient<T>
  {
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly ILogger<T> _logger;

    protected BaseApiClient(
      HttpClient httpClient,
      string baseUrl,
      ILogger<T> logger)
    {
      _httpClient = httpClient;
      _baseUrl = baseUrl;
      _logger = logger;
    }

    public async Task<ApiResponse<TResponse>> ExecuteRequestAsync<TResponse>(IApiRequest<TResponse> request)
    {
      try
      {
        // Build the request url based on the resource and parameters
        var requestUrl = $"{_baseUrl}/{request.Resource}";

        if (request.Parameters is not null)
        {
          requestUrl += $"{request.Parameters.ToQueryString()}";
        }

        // Create the HTTP request and configure it
        var httpRequest = new HttpRequestMessage(request.Method, requestUrl);

        // Add request headers if provided
        if (request.Headers is not null && request.Headers.Any())
        {
          foreach (var header in request.Headers)
          {
            httpRequest.Headers.Add(header.Key, header.Value);
          }
        }

        // Add request body if provided for POST, PUT, or PATCH requests
        if (request.Method == HttpMethod.Post ||
            request.Method == HttpMethod.Put ||
            request.Method == HttpMethod.Patch)
        {
          httpRequest.Content = new StringContent(request.Body, Encoding.UTF8, "application/json");
        }

        // Send the request and handle the response
        var response = await _httpClient.SendAsync(httpRequest);

        if (!response.IsSuccessStatusCode)
        {
          _logger.LogError($"Request to {requestUrl} failed with status code {response.StatusCode}.");
          return new ApiResponse<TResponse>
          {
            HttpStatusCode = response.StatusCode,
            ErrorMessage = $"{request.Method.ToString()} request failed with status code {response.StatusCode}"
          };
        }

        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        if (string.IsNullOrEmpty(content))
        {
          _logger.LogWarning($"Received empty response from {requestUrl}. Status code: {response.StatusCode}");
          return new ApiResponse<TResponse>
          {
            HttpStatusCode = response.StatusCode,
            ErrorMessage = "Empty response received."
          };
        }

        try
        {
          var result = JsonSerializer.Deserialize<TResponse>(content, new JsonSerializerOptions
          {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
          });
          return new ApiResponse<TResponse>
          {
            HttpStatusCode = HttpStatusCode.OK,
            Body = result
          };
        }
        catch (JsonException jsonEx)
        {
          _logger.LogError(jsonEx, "Deserialization failed for response content.");
          return new ApiResponse<TResponse>
          {
            HttpStatusCode = response.StatusCode,
            ErrorMessage = $"Failed to deserialize response to {typeof(TResponse).Name}."
          };
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"An error occurred while executing the {request.Method.ToString()} request.");
        return new ApiResponse<TResponse>
        {
          HttpStatusCode = HttpStatusCode.InternalServerError,
          ErrorMessage = ex.Message
        };
      }
    }
  }
}
