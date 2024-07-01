namespace P4C_Back.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() { }

        public UnauthorizedException(string? message) : base(message) { }

        public UnauthorizedException(string? message, Exception innerException) : base(message, innerException) { }
    }
}
