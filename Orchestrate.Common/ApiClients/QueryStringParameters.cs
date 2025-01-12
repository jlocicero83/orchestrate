namespace Orchestrate.Common.ApiClients
{
  public class QueryStringParameters
  {
    public string Fields { get; set; }
    public string Filter { get; set; }
    public bool IncludeTotalCount { get; set; } = false;
  }
}
