using System.Text.Json.Serialization.Metadata;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Orchestrate.Server.Data.Entities;

namespace Orchestrate.Server.Data
{
  public class TenantDbContext : DbContext
  {
    public TenantDbContext(DbContextOptions<TenantDbContext> options)
      : base(options) { }

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
      
      modelBuilder.Entity<Tenant>().ToTable("tenants").HasQueryFilter(null);
    }
  }
}
