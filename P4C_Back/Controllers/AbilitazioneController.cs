using Microsoft.AspNetCore.Mvc;
using P4C_Back.DTOs.Abilitazione;
using P4C_Back.Models;
using P4C_Back.Services;

namespace P4C_Back.Controllers
{
    [ApiController]
    [Route("abilitazioni/")]
    public class AbilitazioneController(AbilitazioneService abilitazioneService) : ControllerBase
    {
        private readonly AbilitazioneService _abilitazioneService = abilitazioneService;
        
        [HttpGet("getUser")]
        public async Task<ActionResult<AbilitazioneDto>> GetUser()
        {
            string user = (string) HttpContext.Items["User"]!;
            #pragma warning disable CS8600
            Abilitazione abilitazione = await _abilitazioneService.GetAbilitazione(user);
            #pragma warning restore CS8600

            return Ok(new AbilitazioneDto(abilitazione!));
        }
    }
}
