using System.Net;

namespace Orchestrate.Common.ApiClients
{
  public class ApiResponse<T>
  {
    public T Body { get; set; }

    public HttpStatusCode HttpStatusCode { get; set; }

    public bool HasErrors => HttpStatusCode != HttpStatusCode.OK;

    public string ErrorMessage { get; set; }
  }
}
