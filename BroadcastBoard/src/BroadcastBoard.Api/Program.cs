using BroadcastBoard.Api.Controllers;
using BroadcastBoard.Api.Middleware;
using BroadcastBoard.Application.Common.Interfaces;
using BroadcastBoard.Application.Shows.Commands;
using BroadcastBoard.Domain.Common.Interfaces;
using BroadcastBoard.Infrastructure.Logging;
using BroadcastBoard.Infrastructure.Repositories;
using BroadcastBoard.Infrastructure.Services;
using MediatR;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:5000");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BroadcastBoard API",
        Version = "v1"
    });
});
builder.Services.AddMediatR(typeof(CreateShowCommandHandler).Assembly);
builder.Services.AddSingleton<IShowRepository, InMemoryShowRepository>();
builder.Services.AddScoped<INotificationService, DummyNotificationService>();
builder.Services.AddControllers()
    .AddApplicationPart(typeof(ShowsController).Assembly);
builder.Services.Configure<LoggingOptions>(
    builder.Configuration.GetSection("LoggingOptions"));

builder.Services.AddSingleton<IErrorLogger, FileErrorLogger>();

var app = builder.Build();
app.UseMiddleware<ExceptionLoggingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
public partial class Program { }
