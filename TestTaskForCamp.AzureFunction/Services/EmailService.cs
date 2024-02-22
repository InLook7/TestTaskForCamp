using System;
using Azure.Storage.Blobs.Models;
using MimeKit;
using TestTaskForCamp.AzureFunction.Services.Interfaces;

namespace TestTaskForCamp.AzureFunction.Services;

public class EmailService : IEmailService
{
    public MimeMessage GenerateEmailMessage(string name, BlobProperties properties, string sasUrl)
    {
        var message = new MimeMessage();
        
        message.From.Add(new MailboxAddress(Environment.GetEnvironmentVariable("SenderName"), Environment.GetEnvironmentVariable("SenderEmail")));
        message.To.Add(new MailboxAddress("", properties.Metadata["email"]));
        message.Subject = Environment.GetEnvironmentVariable("Subject");
        message.Body = new TextPart("Plain")
        {
            Text = $"File \"{name}\" is successfully uploaded.\n{sasUrl}"
        };

        return message;
    }
}