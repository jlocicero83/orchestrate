using System.Text.Json.Serialization;

namespace Orchestrate.Common.ApiClients.DanielsOrchestralApi
{
  public class SearchDatabaseV3ResponseModel
  {
    public string Composer { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("birth_year")]
    public string BirthYear { get; set; }

    [JsonPropertyName("death_year")]
    public string DeathYear { get; set; }
    public string Title { get; set; }
    public string Id { get; set; }

    [JsonPropertyName("composer_domo_uid")]
    public string ComposerDomoId { get; set; }

    [JsonPropertyName("work_domo_uid")]
    public string WorkDomoId { get; set; }
    public List<SearchDatabaseV3ResponseMovement>? Movements { get; set; }
  }

  public class SearchDatabaseV3ResponseMovement
  {
    public string Order { get; set; }
    public string Duration { get; set; }
    public string Name { get; set; }
  }
}
