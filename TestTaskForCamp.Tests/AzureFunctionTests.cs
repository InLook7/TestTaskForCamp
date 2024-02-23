using Xunit;
using Moq;
using Azure.Storage;
using Azure.Storage.Sas;
using Azure.Storage.Blobs.Models;
using MimeKit;
using TestTaskForCamp.AzureFunction.Services;

namespace TestTaskForCamp.Tests;

public class AzureFunctionTests
{
    [Fact]
    public void GenerateSasToken_ReturnsValidSasToken()
    {
        // Arrange
        var name = "testBlob";
        
        Environment.SetEnvironmentVariable("ContainerName", "*hidden*");
        Environment.SetEnvironmentVariable("AccountName", "*hidden*");
        Environment.SetEnvironmentVariable("AccountKey", "+5coqSmaIPMlmyssPAmXXEyniiH50RHtv4y+Ax71Xc2pP/ycLcJo/y3iMuyWkD0PLHPUm2RvhPdH+AStN+ZEJQ==");

        var tokenService = new TokenService();
        
        // Act
        var actualSasToken = tokenService.GenerateSasToken(name);

        // Assert
        Assert.NotNull(actualSasToken);
    }
    
    [Fact]
    public void GenerateEmailMessage_WithValidData_ReturnsCorrectMessage()
    {
        // Arrange
        var properties = new BlobProperties();
        properties.Metadata.Add("email", "test@gmail.com");
        var url = "https://example.com/test.docx";

        Environment.SetEnvironmentVariable("SenderName", "AzureGigaChad");
        Environment.SetEnvironmentVariable("SenderEmail","forazuretestmy@gmail.com");
        Environment.SetEnvironmentVariable("Subject", "Notification from Azure");
        
        var emailMessageGenerator = new EmailService();
    
        // Act
        var result = emailMessageGenerator.GenerateEmailMessage(properties, url);
    
        // Assert
        Assert.NotNull(result);
    }
}