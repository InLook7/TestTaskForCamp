using Azure.Storage.Blobs;
using TestTaskForCamp.WebApi.Services.Interfaces;

namespace TestTaskForCamp.WebApi.Services;

public class FileService : IFileService
{
    private readonly BlobServiceClient _blobServiceClient;

    public FileService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }
    
    public void UploadFile(IFormFile file, string email)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient("container");
        var blobClient = containerClient.GetBlobClient(file.Name);
        blobClient.Upload(file.OpenReadStream());
        blobClient.SetMetadata(new Dictionary<string,string> {{"email", email}});
    }
}