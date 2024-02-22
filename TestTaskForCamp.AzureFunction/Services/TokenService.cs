using System;
using Azure.Storage;
using Azure.Storage.Sas;
using TestTaskForCamp.AzureFunction.Services.Interfaces;

namespace TestTaskForCamp.AzureFunction.Services;

public class TokenService : ITokenService
{
    public string GenerateSasToken(string name)
    {
        var blobSasBuilder = new BlobSasBuilder()
        {
            BlobContainerName = Environment.GetEnvironmentVariable("ContainerName"),
            BlobName = name,
            ExpiresOn = DateTime.UtcNow.AddMinutes(60),
        };
        
        blobSasBuilder.SetPermissions(BlobSasPermissions.All);

        var credential = new StorageSharedKeyCredential(Environment.GetEnvironmentVariable("AccountName"),
            Environment.GetEnvironmentVariable("AccountKey"));
        var sasToken = blobSasBuilder.ToSasQueryParameters(credential);

        return sasToken.ToString();
    }
}