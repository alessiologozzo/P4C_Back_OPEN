
namespace P4C_Back.DTOs.Piattaforma
{
    public record PiattaformaWithoutRelationsDto
    {
        public int Id { get; set; }

        public string? NomePiattaforma { get; set; }

        public string? DescPiattaforma { get; set; }

        public PiattaformaWithoutRelationsDto(Models.Piattaforma piattaforma)
        {
            Id = piattaforma.Id;
            NomePiattaforma = piattaforma.NomePiattaforma;
            DescPiattaforma = piattaforma.DescPiattaforma;
        }
    }
}
