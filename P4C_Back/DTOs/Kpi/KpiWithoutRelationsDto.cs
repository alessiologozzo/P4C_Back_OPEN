using P4C_Back.Extendables;
using P4C_Back.Extendables.Records;

namespace P4C_Back.DTOs.Kpi
{
    public record KpiWithoutRelationsDto : AuditRecord
    {
        public int Id { get; set; }

        public string? NomeKpi { get; set; }

        public string? DescKpi { get; set; }

        public string? CategoriaKpi { get; set; }

        public string? UMKpi { get; set; }

        public string? Benchmark { get; set; }

        public KpiWithoutRelationsDto(Models.Kpi kpi) : base(kpi.UtenteInserimento, kpi.DataInserimento, kpi.UtenteAggiornamento, kpi.DataAggiornamento)
        {
            Id = kpi.Id;
            NomeKpi = kpi.NomeKpi;
            DescKpi = kpi.DescKpi;
            CategoriaKpi = kpi.CategoriaKpi;
            UMKpi = kpi.UMKpi;
            Benchmark = kpi.Benchmark;
        }
    }
}
