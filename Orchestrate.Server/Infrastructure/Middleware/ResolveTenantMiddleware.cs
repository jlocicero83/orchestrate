using Orchestrate.Server.Services.TenantResolver;

namespace Orchestrate.Server.Infrastructure.Middleware
{
  public class ResolveTenantMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _environment;

    public ResolveTenantMiddleware(RequestDelegate next, 
      IWebHostEnvironment environment)
    {
      _next = next;
      _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context, 
      ITenantResolverService tenantResolverService)
    {
      string tenantId;

      if (_environment.IsDevelopment())
      {
        tenantId = "test";
      }
      else
      {
        tenantId = GetSubdomain(context.Request.Host.Value);
      }
      await tenantResolverService.ResolveTenantAsync(tenantId);
      await _next(context);
    }

    private string GetSubdomain(string host)
    {
      var segments = host.Split('.');

      if (segments.Length < 2) return null;

      return segments[0].ToLower();
    }
  }
}
