namespace P4C_Back.DTOs.Dashboard
{
    public record CsvDto
    {
        public string? NomePiattaforma { get; set; }

        public string? NomeCanale { get; set; }

        public string? PathReport { get; set; }

        public string? Link { get; set; }

        public int? IdReport { get; set; }

        public string? NomeReport { get; set; }

        public string? DescReport { get; set; }

        public string? TipoOggetto { get; set; }

        public string? LivelloAccessibilita { get; set; }

        public int? IdKpi { get; set; }

        public string? NomeKpi { get; set; }

        public string? DescKpi { get; set; }

        public string? CategoriaKpi { get; set; }

        public string? UMKpi { get; set; }

        public string? TipoCriterio { get; set; }

        public int? IdCriterio { get; set; }

        public string? DescCriterio { get; set; }

        public string? DettaglioCriterio { get; set; }
    }
}
