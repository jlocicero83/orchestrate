using Orchestrate.Server.Services.TenantResolver;

namespace Orchestrate.Server.Context
{
  public class TenantContext
  {
    public TenantInfoModel CurrentTenant { get; private set; }

    public void SetCurrentTenant(TenantInfoModel tenant)
    {
      CurrentTenant = tenant;
    }
  }
}
