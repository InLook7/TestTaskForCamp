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
    
    public async Task<string> UploadFileAsync(IBrowserFile file, string email)
    {
        var fileContent = new StreamContent(file.OpenReadStream());
        var multipartContent = new MultipartFormDataContent();
        multipartContent.Add(fileContent, "file", file.Name);
        multipartContent.Add(new StringContent(email), "email");
        
        var response = await _httpClient.PostAsync("api/file/uploadFile/", multipartContent);
        var result = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.OK)
            return "File has uploaded successfully!";
        return "File hasn't uploaded";
    }
}