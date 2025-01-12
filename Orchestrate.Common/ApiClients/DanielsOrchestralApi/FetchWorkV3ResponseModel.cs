using System.Text.Json.Serialization;

namespace Orchestrate.Common.ApiClients.DanielsOrchestralApi
{
  public class FetchWorkV3ResponseModel
  {
    public string Formula { get; set; }
    public FetchWorkV3ResponseComposer Composer { get; set; }
    public string Duration { get; set; }
    public string Title { get; set; }
    public string Remarks { get; set; }

    // TODO: Clean this up once we know what's needed... maybe no Domo Id's
    [JsonPropertyName("domo_uid")]
    public string DomoUID { get; set; }

    [JsonPropertyName("composed_from")]
    public string ComposedFrom { get; set; }

    [JsonPropertyName("composed_to")]
    public string ComposedTo { get; set; }

    public List<FetchWorkV3ResponseMovement>? Movements { get; set; }

    //public FetchWorkV3ResponseEnsemble Ensemble { get; set; }
  }

  public class FetchWorkV3ResponseComposer
  {

    [JsonPropertyName("domo_uid")]
    public string DomoUID { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("birth_year")]
    public string BirthYear { get; set; }

    [JsonPropertyName("death_year")]
    public string DeathYear { get; set; }

    public string Nationality { get; set; }

  }

  // TODO: Response does not match what's in daniel's documentation... 
  // update model when their support responds.
  public class FetchWorkV3ResponseEnsemble
  {
    public string Abbreviation { get; set; }
    public int Count { get; set; }
    public int Order { get; set; }
  }
  public class FetchWorkV3ResponseMovement
  {
    public string Duration { get; set; }
    public string Name { get; set; }
  }
}
