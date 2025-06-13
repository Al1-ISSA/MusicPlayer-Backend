namespace MusicBackend.Exceptions;


public class AlreadyExistsException : Exception
{
    
    public int StatusCode { get; set; } = 400;
    public AlreadyExistsException(string name, string attribute, object value)
        : base($"Entity ({name}) with Attribute ({attribute}) with value ({value}) already exists.")
    {
    }
}