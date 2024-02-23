using Xunit;
using Moq;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestTaskForCamp.WebApi.Controllers;
using TestTaskForCamp.WebApi.Services;
using TestTaskForCamp.WebApi.Services.Interfaces;

namespace TestTaskForCamp.Tests;

public class ControllerTests
{
    private readonly Mock<BlobServiceClient> _mockBlobServiceClient;
    private readonly Mock<BlobContainerClient> _mockBlobContainerClient;
    private readonly Mock<BlobClient> _mockBlobClient;
    private readonly FileService _fileService;
    private readonly FileController _fileController;

    public ControllerTests()
    {
        _mockBlobServiceClient = new Mock<BlobServiceClient>();
        _mockBlobContainerClient = new Mock<BlobContainerClient>();
        _mockBlobClient = new Mock<BlobClient>();
        _fileService = new FileService(_mockBlobServiceClient.Object);
        _fileController = new FileController(_fileService);
    }

    [Fact]
    public void UploadFile_WithValidData_ReturnsOkStatus()
    {
        // Arrange
        var mockFormFile = new Mock<IFormFile>();
        var file = "test.docx";
        var email = "test@gmail.com";
        
        _mockBlobServiceClient.Setup(x => x.GetBlobContainerClient("containerfordocxfiles")).Returns(_mockBlobContainerClient.Object);
        _mockBlobContainerClient.Setup(x => x.GetBlobClient(It.IsAny<string>())).Returns(_mockBlobClient.Object);
        mockFormFile.Setup(f => f.FileName).Returns(file);
        
        // Act
        var result = _fileController.UploadFile(mockFormFile.Object, email);

        // Assert
        Assert.IsType<OkResult>(result);
        _mockBlobClient.Verify(x => x.Upload(It.IsAny<Stream>()), Times.Once);
    }
    
    [Fact]
    public void UploadFile_WithInvalidData_ReturnsStatusCode500()
    {
        // Arrange
        var mockFormFile = new Mock<IFormFile>();
        var file = "test.pdf";
        var email = "test.gmail.com";
        
        _mockBlobServiceClient.Setup(x => x.GetBlobContainerClient("container")).Returns(_mockBlobContainerClient.Object);
        _mockBlobContainerClient.Setup(x => x.GetBlobClient(It.IsAny<string>())).Returns(_mockBlobClient.Object);
        mockFormFile.Setup(f => f.FileName).Returns(file);
        
        // Act
        var result = _fileController.UploadFile(mockFormFile.Object, email);

        // Assert
        Assert.Equal(500, (result as StatusCodeResult)?.StatusCode);
    }
}