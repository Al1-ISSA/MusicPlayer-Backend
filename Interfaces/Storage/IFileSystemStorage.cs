namespace MusicBackend.Interfaces;

public interface IFileSystemStorage
{
    Task<string> SaveFileLocallyAsync(IFormFile file, string fileName);
    

}