namespace TestTaskForCamp.WebApi.Services.Interfaces;

public interface IFileService
{
    void UploadFile(IFormFile file, string email);
}