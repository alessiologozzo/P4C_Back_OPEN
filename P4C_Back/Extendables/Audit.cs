namespace P4C_Back.Extendables
{
    public abstract class Audit
    {
        public string? UtenteInserimento { get; set; }

        public DateTime? DataInserimento { get; set; }

        public string? UtenteAggiornamento { get; set; }

        public DateTime? DataAggiornamento { get; set; }
    }
}
