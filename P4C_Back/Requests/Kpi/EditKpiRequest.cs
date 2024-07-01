namespace P4C_Back.Requests.Kpi
{
    public record EditKpiRequest(int Id, string NomeKpi, string? DescKpi, string? CategoriaKpi, string? UMKpi, string? Benchmark, List<int>? CriterioIds)
    {
    }
}
