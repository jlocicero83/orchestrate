using System.Net;

namespace Orchestrate.Common.Exceptions
{
  public class BadRequestException : BaseException
  {
    public override HttpStatusCode HttpStatusCode { get; } = HttpStatusCode.BadRequest;
    public BadRequestException(string message) : base(message) { }
  }
}
