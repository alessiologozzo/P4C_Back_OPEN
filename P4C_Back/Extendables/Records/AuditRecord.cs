namespace P4C_Back.Extendables.Records
{
    public abstract record AuditRecord(string? UtenteInserimento, DateTime? DataInserimento, string? UtenteAggiornamento, DateTime? DataAggiornamento)
    {
    }
}
