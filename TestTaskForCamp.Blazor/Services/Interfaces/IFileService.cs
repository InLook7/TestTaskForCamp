using Microsoft.AspNetCore.Components.Forms;
using TestTaskForCamp.Blazor.Models;

namespace TestTaskForCamp.Blazor.Services.Interfaces;

public interface IFileService
{
    Task<string> UploadFileAsync(IBrowserFile file, string email);
}