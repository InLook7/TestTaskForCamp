using Azure.Storage.Blobs;
using TestTaskForCamp.WebApi.Services;
using TestTaskForCamp.WebApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetValue<string>("AzureBlobStorageConnectionString")));
builder.Services.AddSingleton<IFileService, FileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{}

app.MapControllers();

app.Run();