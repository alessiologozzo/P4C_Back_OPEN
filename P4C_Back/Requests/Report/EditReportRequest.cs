namespace P4C_Back.Requests.Report
{
    public record EditReportRequest(int Id, string NomeReport, string TipoOggetto, string LivelloAccessibilita, string? DescReport, string? PathReport, string? Link, string? ListaDataset, List<string>? KpiNames, List<string>? CanaleNames)
    {
    }
}
