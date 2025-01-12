namespace Orchestrate.Server.Features.DanielsDatabase.SearchDatabase
{
  public class ComposerWork
  {
    public string Id { get; set; }

    // TODO: Clean this up once we know what's needed... maybe no Domo Id's
    public string ComposerDomoId { get; set; }
    public string WorkDomoId { get; set; }
    public string Composer { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string BirthYear { get; set; }
    public string DeathYear { get; set; }
    public string Title { get; set; }
    public List<ComposerWorkMovement>? Movements { get; set; }
  }

  public class ComposerWorkMovement
  {
    public string Order { get; set; }
    public string Duration { get; set; }
    public string Name { get; set; }
  }
}
