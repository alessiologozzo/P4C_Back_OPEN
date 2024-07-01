using P4C_Back.DTOs.All;
using P4C_Back.DTOs.Criterio;
using P4C_Back.DTOs.Kpi;

namespace P4C_Back.Responses.Kpi
{
    public record IndexKpiResponse(List<KpiDto> Kpis, List<IdNameDto> Criteri, List<EnumDto> Enums) { }
}
