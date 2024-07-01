using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using P4C_Back.Extendables;
using P4C_Back.Requests.Criterio;

namespace P4C_Back.Models
{
    [Table("tbl_criteri")]
    public class Criterio : Audit
    {
        [Column("IdCriterio")]
        [Key]
        public int Id { get; set; }

        public string? TipoCriterio { get; set; }

        public string? DettaglioCriterio { get; set; }

        public string? DescCriterio { get; set; }

        public string? KpiOrigine { get; set; }

        public List<Kpi> Kpis { get; set; } = [];

        public Criterio() { }

        public Criterio(CreateCriterioRequest request, string user)
        {
            TipoCriterio = request.TipoCriterio;
            DettaglioCriterio = request.DettaglioCriterio;
            DescCriterio = request.DescCriterio;
            KpiOrigine = request.KpiOrigine;
            UtenteInserimento = user;
            DataInserimento = DateTime.Now;
        }

        public void Edit(EditCriterioRequest request, string user)
        {
            TipoCriterio = request.TipoCriterio;
            DettaglioCriterio = request.DettaglioCriterio;
            DescCriterio = request.DescCriterio;
            KpiOrigine = request.KpiOrigine;
            UtenteAggiornamento = user;
            DataAggiornamento = DateTime.Now;
        }
    }
}
