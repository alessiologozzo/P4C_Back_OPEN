using P4C_Back.DTOs.Piattaforma;

namespace P4C_Back.DTOs.Canale
{
    public record CanaleWithPiattaformaDto
    {
        public int Id { get; set; }

        public string? NomeCanale { get; set; }

        public string? DescCanale { get; set; }

        public PiattaformaWithoutRelationsDto? Piattaforma { get; set; }

        public CanaleWithPiattaformaDto(Models.Canale canale)
        {
            Id = canale.Id;
            NomeCanale = canale.NomeCanale;
            DescCanale = canale.DescCanale;
            Piattaforma = canale.Piattaforma != null ? new(canale.Piattaforma) : null;
        }
    }
}
