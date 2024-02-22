using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using TestTaskForCamp.AzureFunction.Services;
using TestTaskForCamp.AzureFunction.Services.Interfaces;

[assembly: FunctionsStartup(typeof(TestTaskForCamp.AzureFunction.Startup))]

namespace TestTaskForCamp.AzureFunction;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddSingleton<ITokenService>((s) => {
            return new TokenService();
        });
        builder.Services.AddSingleton<IEmailService>((s) => {
            return new EmailService();
        });
    }
}