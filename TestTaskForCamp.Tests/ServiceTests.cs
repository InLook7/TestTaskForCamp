using Xunit;
using Moq;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using TestTaskForCamp.WebApi.Services;
using TestTaskForCamp.WebApi.Exceptions;

namespace TestTaskForCamp.Tests;

public class ServiceTests
{
    private readonly Mock<BlobServiceClient> _mockBlobServiceClient;
    private readonly Mock<BlobContainerClient> _mockBlobContainerClient;
    private readonly Mock<BlobClient> _mockBlobClient;
    private readonly FileService _fileService;

    public ServiceTests()
    {
        _mockBlobServiceClient = new Mock<BlobServiceClient>();
        _mockBlobContainerClient = new Mock<BlobContainerClient>();
        _mockBlobClient = new Mock<BlobClient>();
        _fileService = new FileService(_mockBlobServiceClient.Object);
    }
    
    [Fact]
    public void UploadFile_WithValidData_ReturnsSuccessfulWriteProcess()
    {
        // Arrange
        var mockFormFile = new Mock<IFormFile>();
        var file = "test.docx";
        var email = "test@gmail.com";
        
        _mockBlobServiceClient.Setup(x => x.GetBlobContainerClient("containerfordocxfiles")).Returns(_mockBlobContainerClient.Object);
        _mockBlobContainerClient.Setup(x => x.GetBlobClient(It.IsAny<string>())).Returns(_mockBlobClient.Object);
        mockFormFile.Setup(f => f.FileName).Returns(file);
        
        // Act
        _fileService.UploadFile(mockFormFile.Object, email);

        // Assert
        _mockBlobClient.Verify(x => x.Upload(It.IsAny<Stream>()), Times.Once);
    }

    [Fact]
    public void UploadFile_WithInvalidFile_ReturnsInvalidFileException()
    {
        // Arrange
        var mockFormFile = new Mock<IFormFile>();
        var file = "test.pdf";
        var email = "test@gmail.com";
        
        mockFormFile.Setup(f => f.FileName).Returns(file);
        
        // Act
        var act = () => _fileService.UploadFile(mockFormFile.Object, email);

        // Assert
        Assert.Throws<InvalidFileException>(act);
    }
    
    [Fact]
    public void UploadFile_WithInvalidEmail_ReturnsInvalidEmailException()
    {
        // Arrange
        var mockFormFile = new Mock<IFormFile>();
        var file = "test.docx";
        var email = "testgmail.com";
        
        mockFormFile.Setup(f => f.FileName).Returns(file);
        
        // Act
        var act = () => _fileService.UploadFile(mockFormFile.Object, email);

        // Assert
        Assert.Throws<InvalidEmailException>(act);
    }
}