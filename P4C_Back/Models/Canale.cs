using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4C_Back.Models
{
    [Table("tbl_canale")]
    public class Canale
    {
        [Column("IdCanale")]
        [Key]
        public int Id { get; set; }

        public string? NomeCanale { get; set; }

        public string? DescCanale { get; set; }

        public List<Report> Reports { get; set; } = [];

        [Column("fk_piattaforma")]
        public int? FkPiattaforma { get; set; }

        public Piattaforma? Piattaforma { get; set; }
    }
}
