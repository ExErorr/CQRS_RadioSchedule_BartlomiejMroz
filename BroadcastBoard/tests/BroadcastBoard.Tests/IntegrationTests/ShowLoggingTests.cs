using System.Net;
using System.Text;
using System.Text.Json;
using BroadcastBoard.Infrastructure.Logging;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit.Abstractions;

namespace BroadcastBoard.Tests.ApplicationTests;
public class ShowControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly string _logFilePath;
    private readonly ITestOutputHelper _output;

    public ShowControllerTests(WebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        var scopeFactory = factory.Services.GetService(typeof(IServiceScopeFactory)) as IServiceScopeFactory;
        using var scope = scopeFactory.CreateScope();
        var options = scope.ServiceProvider.GetRequiredService<IOptions<LoggingOptions>>();

        _logFilePath = options.Value.ErrorLogPath;
        _client = factory.CreateClient();
        _output = output;
        if (File.Exists(_logFilePath))
            File.Delete(_logFilePath);
    }

    [Fact]
    public async Task Should_Log_ShowCollisionException_To_File()
    {
        if (File.Exists(_logFilePath))
            File.Delete(_logFilePath);

        var show1 = new
        {
            Title = "Audycja A",
            Presenter = "Jan",
            StartTime = "2024-01-01T10:00:00",
            DurationMinutes = 60
        };

        var show2 = new
        {
            Title = "Audycja B",
            Presenter = "Anna",
            StartTime = "2024-01-01T10:30:00",
            DurationMinutes = 30
        };

        var content1 = new StringContent(JsonSerializer.Serialize(show1), Encoding.UTF8, "application/json");
        var content2 = new StringContent(JsonSerializer.Serialize(show2), Encoding.UTF8, "application/json");

        await _client.PostAsync("/api/shows", content1);
        var response = await _client.PostAsync("/api/shows", content2);

        Assert.True(File.Exists(_logFilePath), "Log file was not created.");

        var logContent = await File.ReadAllTextAsync(_logFilePath);

        Assert.Contains("koliduje", logContent);
    }

}
