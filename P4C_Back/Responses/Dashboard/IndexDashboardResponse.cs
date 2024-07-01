using P4C_Back.DTOs.All;
using P4C_Back.DTOs.Criterio;
using P4C_Back.DTOs.Kpi;
using P4C_Back.DTOs.Report;

namespace P4C_Back.Responses.Dashboard
{
    public record IndexDashboardResponse(List<ReportWithoutRelationsDto> Reports, List<KpiWithoutRelationsDto> Kpis, List<CriterioDto> Criteri, List<EnumDto> Enums)
    {
    }
}
