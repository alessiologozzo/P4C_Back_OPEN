using P4C_Back.DTOs.All;
using P4C_Back.DTOs.Report;

namespace P4C_Back.Responses.Report
{
    public record IndexReportResponse(List<ReportDto> Reports, List<NameDto> Kpis, List<CanalePiattaformaDto> Canali, List<EnumDto> Enums) { }
}
