using P4C_Back.Extendables.Records;

namespace P4C_Back.DTOs.Criterio
{
    public record CriterioDto : AuditRecord
    {
        public int Id { get; set; }

        public string? TipoCriterio { get; set; }

        public string? DettaglioCriterio { get; set; }

        public string? DescCriterio { get; set; }

        public string? KpiOrigine { get; set; }

        public bool IsDeletable { get; set; }

        public CriterioDto(Models.Criterio criterio) : base(criterio.UtenteInserimento, criterio.DataInserimento, criterio.UtenteAggiornamento, criterio.DataAggiornamento)
        {
            Id = criterio.Id;
            TipoCriterio = criterio.TipoCriterio;
            DettaglioCriterio = criterio.DettaglioCriterio;
            DescCriterio = criterio.DescCriterio;
            KpiOrigine = criterio.KpiOrigine;
            IsDeletable = criterio.Kpis.Count == 0;
        }
    }
}
