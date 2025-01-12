using Orchestrate.Common.ApiClients.DanielsOrchestralApi;

namespace Orchestrate.Server.Features.DanielsDatabase.SearchDatabase
{
  public interface IDanielsV3ToComposerWorkModelMapper
  {
    public List<ComposerWork> Map(List<SearchDatabaseV3ResponseModel> models);
  }

  public class DanielsV3ToComposerWorkModelMapper : IDanielsV3ToComposerWorkModelMapper
  {
    public List<ComposerWork> Map(List<SearchDatabaseV3ResponseModel> models)
    {
      if (models == null || !models.Any())
        return new List<ComposerWork>();

      return models.Select(MapOne).ToList();
    }

    private ComposerWork MapOne(SearchDatabaseV3ResponseModel model)
    {
      return new ComposerWork
      {
        Composer = model.Composer,
        FirstName = model.FirstName,
        LastName = model.LastName,
        BirthYear = model.BirthYear,
        DeathYear = model.DeathYear,
        Title = model.Title,
        Id = model.Id,
        ComposerDomoId = model.ComposerDomoId,
        WorkDomoId = model.WorkDomoId,
        Movements = model.Movements?.Select(m => new ComposerWorkMovement
        {
          Name = m.Name,
          Duration = m.Duration,
          Order = m.Order
        }).ToList() ?? new List<ComposerWorkMovement>()
      };
    }
  }
}
