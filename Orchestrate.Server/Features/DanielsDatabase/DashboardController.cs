using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orchestrate.Server.Services.TenantResolver;

namespace Orchestrate.Server.Features.DanielsDatabase
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        [HttpGet("info")]
        public IActionResult GetInfo()
        {

            TenantInfoModel tenantInfoModel = HttpContext.Items["CurrentTenant"] as TenantInfoModel;

            if (tenantInfoModel == null)
                return Unauthorized();

            return Ok(new
            {
                tenantInfoModel.Name,
                tenantInfoModel.TenantId,
                tenantInfoModel.IsActive
            });
        }
    }
}
