using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using P4C_Back.Extendables;
using P4C_Back.Requests.Kpi;

namespace P4C_Back.Models
{
    [Table("tbl_kpi")]
    public class Kpi : Audit
    {
        [Key]
        [Column("IdKpi")]
        public int Id { get; set; }

        public string? NomeKpi { get; set; }

        public string? DescKpi { get; set; }

        public string? CategoriaKpi { get; set; }

        public string? UMKpi { get; set;}

        public string? Benchmark {  get; set; }

        public List<Criterio> Criteri { get; set; } = [];

        public List<Report> Reports { get; set; } = [];

        public Kpi() { }

        public Kpi(CreateKpiRequest request, string user)
        {
            NomeKpi = request.NomeKpi;
            DescKpi = request.DescKpi;
            CategoriaKpi = request.CategoriaKpi;
            UMKpi = request.UMKpi;
            Benchmark = request.Benchmark;
            UtenteInserimento = user;
            DataInserimento = DateTime.Now;
        }

        public void Edit(EditKpiRequest request, List<Criterio> criteri, string user)
        {
            NomeKpi = request.NomeKpi;
            DescKpi = request.DescKpi;
            CategoriaKpi = request.CategoriaKpi;
            UMKpi = request.UMKpi;
            Benchmark = request.Benchmark;
            Criteri = criteri;
            UtenteAggiornamento = user;
            DataAggiornamento = DateTime.Now;
        }
    }
}
