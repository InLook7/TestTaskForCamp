using TestTaskForCamp.Blazor.Components;
using TestTaskForCamp.Blazor.Services;
using TestTaskForCamp.Blazor.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddSingleton(x => new HttpClient
{
    BaseAddress = builder.Configuration.GetValue<Uri>("AppBackendUrl")
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();