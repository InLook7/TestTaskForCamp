using Azure.Storage.Blobs.Models;
using MimeKit;

namespace TestTaskForCamp.AzureFunction.Services.Interfaces;

public interface IEmailService
{
    MimeMessage GenerateEmailMessage(string name, BlobProperties properties, string sasUrl);
}