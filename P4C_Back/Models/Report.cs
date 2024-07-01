using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using P4C_Back.Extendables;
using P4C_Back.Requests.Report;

namespace P4C_Back.Models
{
    [Table("tbl_report")]
    public class Report : Audit
    {
        [Column("IdReport")]
        [Key]
        public int Id { get; set; }

        public required string TipoOggetto { get; set; }

        public string? LivelloAccessibilita { get; set; }

        public string? NomeReport { get; set; }

        public string? DescReport { get; set; }

        public string? PathReport { get; set; }

        public string? Link { get; set; }

        [Column("Dataset/ReportPadre")]
        public string? ListaDataset { get; set; }

        public List<Kpi> Kpis { get; set; } = [];

        public List<Canale> Canali { get; set; } = [];

        public Report() { }

        [SetsRequiredMembers]
        public Report(CreateReportRequest request, string user)
        {
            NomeReport = request.NomeReport;
            TipoOggetto = request.TipoOggetto;
            LivelloAccessibilita = request.LivelloAccessibilita;
            DescReport = request.DescReport;
            PathReport = request.PathReport;
            Link = request.Link;
            ListaDataset = request.ListaDataset;
            UtenteInserimento = user;
            DataInserimento = DateTime.Now;
        }

        public void Edit(EditReportRequest request, List<Kpi> kpis, List<Canale> canali, string user)
        {
            NomeReport = request.NomeReport;
            TipoOggetto = request.TipoOggetto;
            LivelloAccessibilita = request.LivelloAccessibilita;
            DescReport = request.DescReport;
            PathReport = request.PathReport;
            Link = request.Link;
            ListaDataset = request.ListaDataset;
            Kpis = kpis;
            Canali = canali;
            UtenteAggiornamento = user;
            DataAggiornamento = DateTime.Now;
        }
    }
}
