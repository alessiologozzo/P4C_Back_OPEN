namespace P4C_Back.DTOs.All
{
    public record EnumDto
    {
        public string FieldName { get; set; }

        public List<string> Values { get; set; }

        public EnumDto(string fieldName, string stringValues)
        {
            FieldName = fieldName;
            Values = [.. stringValues.Split("|")];
        }
    }
}
