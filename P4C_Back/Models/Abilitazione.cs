using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4C_Back.Models
{
    [Table("tbl_abilitazioni")]
    public class Abilitazione
    {
        [Key]
        public required string Utente { get; set; }

        public string? NomeUtente { get; set; }

        public required bool FlgAttivo { get; set; }
    }
}
