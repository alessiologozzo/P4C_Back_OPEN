namespace P4C_Back.DTOs.Canale
{
    public record CanaleWithoutRelationsDto
    {
        public int Id { get; set; }

        public string? NomeCanale { get; set; }

        public string? DescCanale { get; set; }

        public CanaleWithoutRelationsDto(Models.Canale canale)
        {
            Id = canale.Id;
            NomeCanale = canale.NomeCanale;
            DescCanale = canale.DescCanale;
        }
    }
}
