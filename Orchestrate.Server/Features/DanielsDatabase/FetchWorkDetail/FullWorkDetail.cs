namespace Orchestrate.Server.Features.DanielsDatabase.FetchWorkDetail
{
  public class FullWorkDetail
  {
    public string DomoUID { get; set; }
    public string Formula { get; set; }
    public FullWorkDetailComposer Composer { get; set; }
    public string Duration { get; set; }
    public string Title { get; set; }
    public string Remarks { get; set; }
    public string ComposedFrom { get; set; }
    public string ComposedTo { get; set; }

    public List<FullWorkDetailMovement>? Movements { get; set; }
  }

  public class FullWorkDetailComposer
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string BirthYear { get; set; }
    public string DeathYear { get; set; }
    public string Nationality { get; set; }
    public string DomoUID { get; set; }
  }

  public class FullWorkDetailEnsemble
  {
    public string Abbreviation { get; set; }
    public int Count { get; set; }
    public int Order { get; set; }
  }
  public class FullWorkDetailMovement
  {
    public string Name { get; set; }
    public string Duration { get; set; }
  }
}
