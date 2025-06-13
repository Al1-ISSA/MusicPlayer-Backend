using Firebase.Storage;
using MusicBackend.Interfaces;

namespace MusicBackend.Services;

public class FirebaseStorageService : IFirebaseStorageService
{

    private readonly string _bucketName = "musicplayer-44afc.appspot.com";
    private readonly FirebaseStorage _storage;

    public FirebaseStorageService()
    {
        _storage = new FirebaseStorage(_bucketName);
    }
    
  
    public async Task<string> SaveFileToCloudAsync(IFormFile file, string fileName,string folderName)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is empty", nameof(file));

        try
        {
            
            var fileRef = _storage.Child(folderName).Child(fileName);
            
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0; 
                
                var downloadUrl = await fileRef.PutAsync(memoryStream);
                return downloadUrl;
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error uploading file", ex);
        }
    }

    public async Task DeleteFileFromCloudAsync(string fileName, string folderName)
    {
        try
        {
            var fileRef = _storage.Child(folderName).Child(fileName);
            await fileRef.DeleteAsync();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error deleting file", ex);
        }
    }
}