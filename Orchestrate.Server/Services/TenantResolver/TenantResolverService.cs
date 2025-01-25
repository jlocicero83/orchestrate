using Microsoft.EntityFrameworkCore;
using Orchestrate.Common.Exceptions;
using Orchestrate.Server.Data;

namespace Orchestrate.Server.Services.TenantResolver
{
  public interface ITenantResolverService
  {
    Task ResolveTenantAsync(string subdomain);
    public TenantInfoModel CurrentTenant { get; }
  }

  public class TenantResolverService : ITenantResolverService
  {
    private readonly TenantDbContext _dbContext;

    public TenantResolverService(TenantDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public TenantInfoModel CurrentTenant { get; set; }

    public async Task ResolveTenantAsync(string subdomain)
    {
      if (string.IsNullOrWhiteSpace(subdomain))
        throw new BadRequestException("Invalid subdomain.");

      // TODO: Add caching here eventually

      var tenant = await _dbContext.Tenants
        .Where(t => t.TenantId == subdomain && t.IsActive)
        .Select(t => new TenantInfoModel
        {
          TenantId = t.TenantId,
          Name = t.Name,
          IsActive = t.IsActive,
        })
        .FirstOrDefaultAsync();
      
      if (tenant is null)
      {
        throw new TenantDoesNotExistException($"An active tenant was not found with the subdomain '{subdomain}'.");
      }

      CurrentTenant = tenant;
    }

  }
}
