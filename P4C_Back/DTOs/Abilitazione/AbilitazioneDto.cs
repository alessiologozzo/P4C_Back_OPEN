namespace P4C_Back.DTOs.Abilitazione
{
    public record AbilitazioneDto
    {
        public string Utente { get; set; }
        
        public string? NomeUtente { get; set; }

        public bool FlgAttivo { get; set; }

        public AbilitazioneDto(Models.Abilitazione abilitazione) {
            Utente = abilitazione.Utente;
            NomeUtente = abilitazione.NomeUtente;
            FlgAttivo = abilitazione.FlgAttivo;
        }
    }
}
