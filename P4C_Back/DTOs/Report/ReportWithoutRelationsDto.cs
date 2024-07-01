using P4C_Back.DTOs.Canale;
using P4C_Back.DTOs.Kpi;
using P4C_Back.Extendables.Records;

namespace P4C_Back.DTOs.Report
{
    public record ReportWithoutRelationsDto : AuditRecord
    {
        public int Id { get; set; }

        public string TipoOggetto { get; set; }

        public string? LivelloAccessibilita { get; set; }

        public string? NomeReport { get; set; }

        public string? DescReport { get; set; }

        public string? PathReport { get; set; }

        public string? Link { get; set; }

        public string? ListaDataset { get; set; }

        public ReportWithoutRelationsDto(Models.Report report) : base(report.UtenteInserimento, report.DataInserimento, report.UtenteAggiornamento, report.DataAggiornamento)
        {
            Id = report.Id;
            TipoOggetto = report.TipoOggetto;
            LivelloAccessibilita = report.LivelloAccessibilita;
            NomeReport = report.NomeReport;
            DescReport = report.DescReport;
            PathReport = report.PathReport;
            Link = report.Link;
            ListaDataset = report.ListaDataset;
        }
    }
}
