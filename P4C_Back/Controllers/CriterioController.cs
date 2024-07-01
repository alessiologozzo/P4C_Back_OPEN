using Microsoft.AspNetCore.Mvc;
using P4C_Back.DTOs.Criterio;
using P4C_Back.Requests.All;
using P4C_Back.Requests.Criterio;
using P4C_Back.Responses.All;
using P4C_Back.Responses.Criterio;
using P4C_Back.Services;

namespace P4C_Back.Controllers
{
    [ApiController]
    [Route("criteri/")]
    public class CriterioController(CriterioService criterioService) : ControllerBase
    {
        private readonly CriterioService _criterioService = criterioService;

        [HttpGet("index")]
        public async Task<ActionResult<IndexCriterioResponse>> Index()
        {
            IndexCriterioResponse response = await _criterioService.PrepareIndexResponse();

            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<ActionResult<CriterioDto>> Create(CreateCriterioRequest request)
        {
            string user = (string) HttpContext.Items["User"]!;
            CriterioDto criterioDto = await _criterioService.Create(request, user);

            return Created(Request.Path.Value, criterioDto);
        }

        [HttpPut("edit")]
        public async Task<ActionResult<CriterioDto>> Edit(EditCriterioRequest request)
        {
            string user = (string) HttpContext.Items["User"]!;
            CriterioDto criterioDto = await _criterioService.Edit(request, user);

            return Ok(criterioDto);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<IdResponse>> Delete(IdRequest request)
        {
            IdResponse idResponse = await _criterioService.Delete(request.Id);

            return Ok(idResponse);
        }
    }
}
