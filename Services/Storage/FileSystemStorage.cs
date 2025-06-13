using Firebase.Storage;
using MusicBackend.Interfaces;

namespace MusicBackend.Services;

public class FileSystemStorage : IFileSystemStorage
{
    private readonly string _rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");


    public async Task<string> SaveFileLocallyAsync(IFormFile file, string fileName)
    {
        var filePath = Path.Combine(_rootPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return filePath;
        
    }

}