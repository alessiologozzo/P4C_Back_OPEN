using System.ComponentModel.DataAnnotations.Schema;

namespace P4C_Back.Models
{
    [Table("tbl_valorienum")]
    public class ValoreEnum
    {
        public string? NomeCampoEnum { get; set; }

        public string? ValoriCampoEnum { get; set; }
    }
}
