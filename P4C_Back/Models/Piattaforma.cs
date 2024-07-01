using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4C_Back.Models
{
    [Table("tbl_piattaforma")]
    public class Piattaforma
    {
        [Column("IdPiattaforma")]
        [Key]
        public int Id { get; set; }

        public string? NomePiattaforma { get; set; }

        public string? DescPiattaforma { get; set; }

        public List<Canale> Canali { get; set; } = []; 
    }
}
