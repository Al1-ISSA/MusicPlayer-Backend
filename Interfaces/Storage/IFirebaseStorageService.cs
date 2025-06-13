namespace MusicBackend.Interfaces;

public interface IFirebaseStorageService
{
    Task<string> SaveFileToCloudAsync(IFormFile file, string fileName, string folderName);
    Task DeleteFileFromCloudAsync(string fileName, string folderName);

}