using Azure.Storage.Blobs;
using TestTaskForCamp.WebApi.Exceptions;
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
        if (!Validation.IsFileValid(file))
            throw new InvalidFileException("File is invalid");
        
        if (!Validation.IsEmailValid(email))
            throw new InvalidEmailException("Email is invalid");
        
        var guid = Guid.NewGuid(); // To make file name unique
        var containerClient = _blobServiceClient.GetBlobContainerClient("containerfordocxfiles");
        var blobClient = containerClient.GetBlobClient(guid.ToString() + ".docx");
        
        blobClient.Upload(file.OpenReadStream());
        blobClient.SetMetadata(new Dictionary<string,string>
        {
            { "file", file.FileName },
            { "email", email }
        });
    }
}