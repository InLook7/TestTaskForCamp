namespace TestTaskForCamp.AzureFunction.Services.Interfaces;

public interface ITokenService
{
    string GenerateSasToken(string name);
}