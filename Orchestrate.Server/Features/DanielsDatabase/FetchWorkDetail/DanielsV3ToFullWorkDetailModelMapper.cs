using Orchestrate.Common.ApiClients.DanielsOrchestralApi;

namespace Orchestrate.Server.Features.DanielsDatabase.FetchWorkDetail
{
  public interface IDanielsV3ToFullWorkDetailModelMapper
  {
    public FullWorkDetail Map(FetchWorkV3ResponseModel model);
  }
  public class DanielsV3ToFullWorkDetailModelMapper : IDanielsV3ToFullWorkDetailModelMapper
  {
    public FullWorkDetail Map(FetchWorkV3ResponseModel model)
    {
      return new FullWorkDetail
      {
        DomoUID = model.DomoUID,
        Formula = model.Formula,
        Composer = new FullWorkDetailComposer
        {
          DomoUID = model.Composer.DomoUID,
          FirstName = model.Composer.FirstName,
          LastName = model.Composer.LastName,
          BirthYear = model.Composer.BirthYear,
          DeathYear = model.Composer.DeathYear,
          Nationality = model.Composer.Nationality,
        },
        Duration = model.Duration,
        Title = model.Title,
        Remarks = model.Remarks,
        ComposedFrom = model.ComposedFrom,
        ComposedTo = model.ComposedTo,
        Movements = model.Movements?.Select(m => new FullWorkDetailMovement
        {
          Name = m.Name,
          Duration = m.Duration,
        }).ToList() ?? new List<FullWorkDetailMovement>()
      };
    }
  }
}
