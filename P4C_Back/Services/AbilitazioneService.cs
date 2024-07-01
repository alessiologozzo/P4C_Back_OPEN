using System.Security;
using P4C_Back.Data;
using P4C_Back.Exceptions;
using P4C_Back.Models;

namespace P4C_Back.Services
{
    public class AbilitazioneService(AppDbContext appDbContext)
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public string GetFormattedUserOrThrow(string user)
        {
            string formattedUser;
            string[]? splittedUser = user?.Split('\\');
            if (splittedUser != null && splittedUser.Length == 2)
            {
                formattedUser = splittedUser[1];
            }
            else
            {
                throw new UnauthorizedException("Couldn't parse username. Invalid Credentials.");
            }

            return formattedUser;
        }

        public async Task<Abilitazione?> GetAbilitazione(string user)
        {
            Abilitazione? abilitazione = await _appDbContext.Abilitazioni.FindAsync(user);

            return abilitazione;
        }
    }
}
