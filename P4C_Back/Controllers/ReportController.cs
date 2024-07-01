using Microsoft.AspNetCore.Mvc;
using P4C_Back.DTOs.Report;
using P4C_Back.Requests.All;
using P4C_Back.Requests.Report;
using P4C_Back.Responses.All;
using P4C_Back.Responses.Report;
using P4C_Back.Services;

namespace P4C_Back.Controllers
{
    [ApiController]
    [Route("reports/")]
    public class ReportController(ReportService reportService) : ControllerBase
    {
        private readonly ReportService _reportService = reportService;

        [HttpGet("index")]
        public async Task<ActionResult<IndexReportResponse>> Index()
        {
            IndexReportResponse response = await _reportService.PrepareIndexResponse();

            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<ActionResult<ReportDto>> Create(CreateReportRequest reportRequest) {
            string user = (string) HttpContext.Items["User"]!;
            ReportDto report = await _reportService.Create(reportRequest, user);

            return Created(Request.Path.Value, report);
        }

        [HttpPut("edit")]
        public async Task<ActionResult<ReportDto>> Edit(EditReportRequest reportRequest)
        {
            string user = (string)HttpContext.Items["User"]!;
            ReportDto report = await _reportService.Edit(reportRequest, user);

            return Ok(report);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<IdResponse>> Delete(IdRequest request)
        {
            IdResponse idResponse = await _reportService.Delete(request.Id);

            return Ok(idResponse);
        }
    }
}
