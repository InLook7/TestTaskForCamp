using Microsoft.AspNetCore.Mvc;
using TestTaskForCamp.WebApi.Services.Interfaces;

namespace TestTaskForCamp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;
    
    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }
    
    [HttpPost("uploadFile")]
    public IActionResult UploadFile()
    {
        var file = Request.Form.Files[0];
        var email = Request.Form["email"].ToString();

        try
        {
            _fileService.UploadFile(file, email);
            return Ok();
        }
        catch
        {
            return StatusCode(500);
        }
    }
}