using BroadcastBoard.Api.Controllers;
using BroadcastBoard.Application.Common.Interfaces;
using BroadcastBoard.Application.Shows.Commands;
using BroadcastBoard.Domain.Common.Interfaces;
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

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();

app.MapGet("/weatherforecast", () =>
{
    var summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        )).ToArray();

    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
