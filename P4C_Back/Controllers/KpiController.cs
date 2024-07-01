using Microsoft.AspNetCore.Mvc;
using P4C_Back.DTOs.Kpi;
using P4C_Back.Requests.All;
using P4C_Back.Requests.Kpi;
using P4C_Back.Responses.All;
using P4C_Back.Responses.Kpi;
using P4C_Back.Services;

namespace P4C_Back.Controllers
{
    [ApiController]
    [Route("kpis/")]
    public class KpiController(KpiService kpiService) : ControllerBase
    {
        private readonly KpiService _kpiService = kpiService;

        [HttpGet("index")]
        public async Task<ActionResult<IndexKpiResponse>> Index()
        {
            IndexKpiResponse indexKpiResponse = await _kpiService.PrepareIndexResponse();
            
            return Ok(indexKpiResponse);
        }

        [HttpPost("create")]
        public async Task<ActionResult<KpiDto>> Create(CreateKpiRequest request)
        {
            string user = (string) HttpContext.Items["User"]!;
            KpiDto kpiDto = await _kpiService.Create(request, user);

            return Created(Request.Path.Value, kpiDto);
        }

        [HttpPut("edit")]
        public async Task<ActionResult<KpiDto>> Edit(EditKpiRequest request)
        {
            string user = (string)HttpContext.Items["User"]!;
            KpiDto kpiDto = await _kpiService.Edit(request, user);

            return Ok(kpiDto);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<IdResponse>> Delete(IdRequest request)
        {
            IdResponse idResponse = await _kpiService.Delete(request.Id);

            return Ok(idResponse);
        }
    }
}
