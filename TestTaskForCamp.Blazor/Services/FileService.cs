using System.Net;
using Microsoft.AspNetCore.Components.Forms;
using TestTaskForCamp.Blazor.Models;
using TestTaskForCamp.Blazor.Services.Interfaces;

namespace TestTaskForCamp.Blazor.Services;

public class FileService : IFileService
{
    private readonly HttpClient _httpClient;

    public FileService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public string UploadFile(InputDataModel inputDataModel)
    {
        var multipartContent = new MultipartFormDataContent();
        var fileContent = new StreamContent(inputDataModel.File.OpenReadStream());
        multipartContent.Add(fileContent, $"{inputDataModel.File.Name}", inputDataModel.File.Name);
        multipartContent.Add(new StringContent(inputDataModel.Email), "email");
        
        var response = _httpClient.PostAsync("api/file/uploadFile/", multipartContent);
        
        if (response.Result.StatusCode == HttpStatusCode.OK)
            return "File has uploaded successfully!";
        return "File with the same name already exists!";
    }
}