using Orchestrate.Common.Exceptions;
using System.Text.Json;

namespace Orchestrate.Server.Infrastructure.Middleware
{
  public class ExceptionHandlingMiddleware
  {
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (BaseException ex)
      {
        await HandleExceptionAsync(context, ex);
      }
    }

    private static async Task HandleExceptionAsync(HttpContext context, BaseException ex)
    {
      context.Response.ContentType = "application/json";
      context.Response.StatusCode = (int)ex.HttpStatusCode;

      var result = new
      {
        statusCode = (int)ex.HttpStatusCode,
        errorCode = ex.ErrorCode,
        message = ex.Message,
        timestamp = ex.TimeStamp
      };

      var jsonFormattedResponse = JsonSerializer.Serialize(result);
      await context.Response.WriteAsync(jsonFormattedResponse);
    }
  }
}
