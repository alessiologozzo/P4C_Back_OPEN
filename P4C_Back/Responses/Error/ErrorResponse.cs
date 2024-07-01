namespace P4C_Back.Responses.Error
{
    public record ErrorResponse(string Message, int Status, string Url, string Date) { }
}
