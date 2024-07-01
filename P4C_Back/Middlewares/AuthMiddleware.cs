using P4C_Back.Exceptions;
using P4C_Back.Models;
using P4C_Back.Services;


namespace P4C_Back.Middlewares
{
    public class AuthMiddleware(AbilitazioneService abilitazioneService) : IMiddleware
    {
        private readonly AbilitazioneService _abilitazioneService = abilitazioneService;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string user = context.User.Identity?.Name ?? throw new UnauthorizedException("Couldn't get username");
            string formattedUser = _abilitazioneService.GetFormattedUserOrThrow(user);
            Abilitazione abilitazione = await _abilitazioneService.GetAbilitazione(formattedUser) ?? throw new UnauthorizedException("User doesn't exist.");
            if (context.Request.Method != "GET")
            {
                if(!abilitazione.FlgAttivo)
                {
                    throw new ForbiddenException("User needs admin privileges to access this resource.");
                }
            }
            context.Items["User"] = abilitazione.Utente;

            await next(context);
        }
    }
}
