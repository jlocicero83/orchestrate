using System.Net;

namespace Orchestrate.Common.Exceptions
{
  public class TenantDoesNotExistException : BaseException
  {
    public override HttpStatusCode HttpStatusCode { get; } = HttpStatusCode.NotFound;
    public TenantDoesNotExistException(string message) : base(message) { }

  }
}
