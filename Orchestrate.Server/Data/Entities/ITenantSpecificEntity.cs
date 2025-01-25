namespace Orchestrate.Server.Data.Entities
{
  public interface ITenantSpecificEntity
  {
    public string TenantId { get; set; }
  }
}
