using Microsoft.EntityFrameworkCore;
using Orchestrate.Server.Data.Entities;

namespace Orchestrate.Server.Data
{
  public class OrchestrateDbContext : DbContext
  {
    public OrchestrateDbContext(DbContextOptions<OrchestrateDbContext> options)
      : base(options) { }

    public DbSet<Person> People { get; set; }
  }
}
