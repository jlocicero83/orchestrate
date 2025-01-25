using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Microsoft.EntityFrameworkCore;
using Orchestrate.Server.Data.Entities;
using Orchestrate.Server.Services.TenantResolver;

namespace Orchestrate.Server.Data
{
  public class OrchestrateDbContext : DbContext
  {
    private readonly ITenantResolverService _tenantResolverService;
    public TenantInfoModel CurrentTenant; 

    public OrchestrateDbContext(
      DbContextOptions<OrchestrateDbContext> options, ITenantResolverService tenantResolverService)
      : base(options) 
    {
      _tenantResolverService = tenantResolverService;
      CurrentTenant = _tenantResolverService.CurrentTenant;
    }

    public DbSet<Tenant> Tenants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      var serializerSettings = new JsonSerializerOptions
      {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        TypeInfoResolver = new DefaultJsonTypeInfoResolver()
      };
      serializerSettings.Converters.Add(new JsonStringEnumConverter());

      //base.OnModelCreating(modelBuilder);

      // Configure entities with global query filters for tenant data isolation
      modelBuilder.Entity<Person>(person =>
      {
        person.HasQueryFilter(p => p.TenantId == CurrentTenant.TenantId);

        person.Property(p => p.Address)
          .HasColumnType("jsonb")
          .HasConversion(
            x => JsonSerializer.Serialize(x, serializerSettings),
            x => JsonSerializer.Deserialize<Address>(x, serializerSettings)
            );

        person.Property(p => p.FirstName).IsRequired();
        person.Property(p => p.LastName).IsRequired();
        person.Property(p => p.IsActive).HasDefaultValue(true).IsRequired();
      });

      modelBuilder.Entity<Tenant>().HasQueryFilter(null);
    }

  }
}
