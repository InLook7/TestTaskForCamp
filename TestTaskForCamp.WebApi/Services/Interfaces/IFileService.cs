namespace TestTaskForCamp.WebApi.Services.Interfaces;

public interface IFileService
{
    void UploadFileAsync(IFormFile file, string email);
}