namespace MusicBackend.Exceptions;

public class BadRequestException : Exception
{
    public int StatusCode { get; set; } = 400;
    public BadRequestException(string message) : base(message)
    {
    }
}