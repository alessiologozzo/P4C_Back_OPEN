namespace P4C_Back.Requests.Criterio
{
    public record CreateCriterioRequest(string? TipoCriterio, string DettaglioCriterio, string DescCriterio, string? KpiOrigine)
    {
    }
}
