namespace P4C_Back.Requests.Kpi
{
    public record CreateKpiRequest(string NomeKpi, string? DescKpi, string? CategoriaKpi, string? UMKpi, string? Benchmark, List<int>? CriterioIds)
    {
    }
}
