namespace Orchestrate.Common.ApiClients
{
  public interface IApiRequest<TResponse>
  {
    HttpMethod Method { get; }
    string BaseUrl { get; }
    string Resource { get; }
    QueryStringParameters Parameters { get; }
    IDictionary<string, string> Headers { get; }
    string Body { get; }
  }

  public class ApiRequest<TResponse> : IApiRequest<TResponse>
  {
    public HttpMethod Method { get; set; }
    public string BaseUrl { get; set; }
    public string Resource { get; set; }
    public QueryStringParameters Parameters { get; set; }
    public IDictionary<string, string> Headers { get; set; }
    public string Body { get; set; }

    public ApiRequest()
    {
      Headers = new Dictionary<string, string>();
    }
  }
}
