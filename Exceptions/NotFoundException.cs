namespace MusicBackend.Exceptions;

public class NotFoundException : Exception
{
    
    public int StatusCode { get; set; } = 404;
    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }
}