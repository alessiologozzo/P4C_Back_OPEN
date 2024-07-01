using P4C_Back.DTOs.All;
using P4C_Back.DTOs.Criterio;

namespace P4C_Back.Responses.Criterio
{
    public record IndexCriterioResponse(List<CriterioDto> Criteri, List<IdNameDto> KpiIdNames, EnumDto? Enum) { }
}
