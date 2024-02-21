using Microsoft.AspNetCore.Mvc;

namespace TestTaskForCamp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    [HttpPost("uploadFile")]
    public string UploadFile()
    {
        return "";
    }
}