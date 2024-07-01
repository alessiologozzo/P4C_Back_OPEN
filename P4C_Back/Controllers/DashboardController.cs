using Microsoft.AspNetCore.Mvc;
using P4C_Back.Responses.Dashboard;
using P4C_Back.Services;

namespace P4C_Back.Controllers
{
    [ApiController]
    [Route("dashboard/")]
    public class DashboardController(DashboardService dashboardService) : ControllerBase
    {
        private readonly DashboardService _dashboardService = dashboardService;

        [HttpGet("getCsvFile")]
        public async Task<FileResult> GetCsvFile()
        {
            byte[] bytes = await _dashboardService.GetCsvFileBytes();

            return File(bytes, "text/csv", $"data.csv");
        }

        [HttpGet("index")]
        public async Task<ActionResult<IndexDashboardResponse>> Index()
        {
            IndexDashboardResponse response = await _dashboardService.Index();

            return Ok(response);
        }
    }
}
