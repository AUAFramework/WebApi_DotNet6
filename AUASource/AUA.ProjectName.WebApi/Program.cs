using AUA.ProjectName.WebApi.AppConfiguration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configuration();

builder.Services.AddControllers();

var app = builder.Build();

app.Configuration();

app.MapControllers();

app.Run();
