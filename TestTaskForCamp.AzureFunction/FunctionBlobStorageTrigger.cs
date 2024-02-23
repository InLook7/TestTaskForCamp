using System;
using System.IO;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using MailKit.Net.Smtp;
using TestTaskForCamp.AzureFunction.Services.Interfaces;

namespace TestTaskForCamp.AzureFunction;

[StorageAccount("AzureBlobStorageConnectionString")]
public class FunctionBlobStorageTrigger
{
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    
    public FunctionBlobStorageTrigger(ITokenService tokenService, IEmailService emailService)
    {
        _tokenService = tokenService;
        _emailService = emailService;
    }
    
    [FunctionName("SendEmail")]
    public void Run(
        [BlobTrigger("containerfordocxfiles/{name}")] Stream myBlob, string name, BlobProperties properties, Uri uri, ILogger log)
    {
        var sasToken = _tokenService.GenerateSasToken(name);
        var sasUrl = uri.AbsoluteUri + "?" + sasToken;

        var message = _emailService.GenerateEmailMessage(properties, sasUrl);
        
        using var client = new SmtpClient();
        client.Connect(Environment.GetEnvironmentVariable("Server"), 465, true);
        client.Authenticate(Environment.GetEnvironmentVariable("UsernameEmail"), Environment.GetEnvironmentVariable("PasswordEmail"));
        client.Send(message);
        client.Disconnect(true);
    }
}