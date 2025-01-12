using Microsoft.AspNetCore.Mvc;
using Orchestrate.Server.Features.DanielsDatabase.FetchWorkDetail;
using Orchestrate.Server.Features.DanielsDatabase.SearchDatabase;

namespace Orchestrate.Server.Features.DanielsDatabase
{
  [ApiController]
  [Route("danielsDatabase")]
  public class DanielsDatabaseController(
    ISearchDatabaseRequestHandler searchDatabaseRequestHandler,
    IFetchWorkDetailRequestHandler fetchWorkDetailRequestHandler) : Controller
  {
    private readonly ISearchDatabaseRequestHandler _searchDatabaseRequestHandler = searchDatabaseRequestHandler;
    private readonly IFetchWorkDetailRequestHandler _fetchWorkDetailRequestHandler = fetchWorkDetailRequestHandler;

    /// <summary>
    /// Finds works in Daniels database by composer and/or work title.
    /// </summary>
    /// <param name="composer"></param>
    /// <param name="work"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> SearchComposerWorksAsync(CancellationToken token, [FromQuery] string composer = null, [FromQuery] string work = null)
    {
      var request = new SearchDatabaseRequest { Composer = composer, Work = work };

      var result = await _searchDatabaseRequestHandler.SearchDatabaseAsync(request, token);

      return Ok(result);
    }

    /// <summary>
    /// Fetches a single work from Daniels database by work id (not domo id).
    /// </summary>
    /// <param name="composer"></param>
    /// <param name="work"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("fetchWork/{workId}")]
    public async Task<IActionResult> GetFullWorkDetail([FromRoute] string workId, CancellationToken token)
    {
      var request = new FetchWorkDetailRequest { WorkId = workId };

      var result = await _fetchWorkDetailRequestHandler.FetchWorkAsync(request, token);

      return Ok(result);
    }
  }
}
