using System.Net;

namespace Orchestrate.Common.Exceptions
{
  public abstract class BaseException : Exception
  {
    public virtual HttpStatusCode HttpStatusCode { get; } = HttpStatusCode.InternalServerError;
    public virtual string ErrorCode => HttpStatusCode.ToString();
    public virtual string ErrorMessage { get; } = "An unexpected error occured. Please try again later.";
    public DateTime TimeStamp { get; } = DateTime.UtcNow;


    public BaseException() { }

    public BaseException(string message) : base(message) { }

    public BaseException(string message, Exception inner) : base(message, inner) { }

  }
}
