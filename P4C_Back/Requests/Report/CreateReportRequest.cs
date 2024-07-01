using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace P4C_Back.Requests.Report
{
    public record CreateReportRequest(string NomeReport, string TipoOggetto, string LivelloAccessibilita, string? DescReport, string? PathReport, string? Link, string? ListaDataset, List<string>? KpiNames, List<string>? CanaleNames)
    {
        
    }
}
