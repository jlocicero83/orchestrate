namespace Orchestrate.Server.Features.DanielsDatabase.FetchWorkDetail
{
  public class FetchWorkDetailRequest
  {
    /// <summary>
    /// The id of the work to be fetched. Not to be confused with workDomoId
    /// </summary>
    public string WorkId { get; set; }
  }
}
