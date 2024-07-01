namespace P4C_Back.Requests.Criterio
{
    public record EditCriterioRequest(int Id, string? TipoCriterio, string DettaglioCriterio, string DescCriterio, string? KpiOrigine)
    {
    }
}
