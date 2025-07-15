using BroadcastBoard.Domain.Entities;
using BroadcastBoard.Application.Shows.Commands;
using BroadcastBoard.Application.Common.Interfaces;
using BroadcastBoard.Domain.Common.Interfaces;
using BroadcastBoard.Application.Shows.Exceptions;
using BroadcastBoard.Infrastructure.Repositories;
using BroadcastBoard.Infrastructure.Services;

namespace BroadcastBoard.Tests.ApplicationTests;

public class CreateShowTests
{
    [Fact]
    public async Task Should_Create_Show_When_No_Collision()
    {
        var repo = new InMemoryShowRepository();
        var notification = new DummyNotificationService();
        var handler = new CreateShowCommandHandler(repo, notification);

        var command = new CreateShowCommand("Tytuł", "Prezenter", new DateTime(2024, 1, 1, 10, 0, 0), 60);

        var result = await handler.Handle(command, default);

        Assert.NotNull(result);
        Assert.Equal("Tytuł", result.Title);
    }

    [Fact]
    public async Task Should_Throw_When_Show_Collides()
    {
        var repo = new InMemoryShowRepository();
        var notification = new DummyNotificationService();
        var handler = new CreateShowCommandHandler(repo, notification);

        var command = new CreateShowCommand("Nowa", "B", new DateTime(2024, 1, 1, 10, 30, 0), 30);
        var command2 = new CreateShowCommand("Nowa", "B", new DateTime(2024, 1, 1, 10, 49, 0), 30);
        handler.Handle(command, default).Wait();
        await Assert.ThrowsAsync<ShowCollisionException>(() => handler.Handle(command2, default));
    }
    [Fact]
    public async Task Should_Invoke_NotificationService_When_Show_Created()
    {
        var repo = new InMemoryShowRepository();
        var notification = new DummyNotificationService();
        var handler = new CreateShowCommandHandler(repo, notification);

        var command = new CreateShowCommand("Tytuł", "Prezenter", new DateTime(2024, 1, 1, 10, 0, 0), 60);

        var result = await handler.Handle(command, default);

        Assert.NotNull(result);
        Assert.True(notification.NotifyCalled, "Notification service was not called");
    }
    [Fact]
    public async Task Should_Return_Correct_ShowDto_After_Creation()
    {
        var repo = new InMemoryShowRepository();
        var notification = new DummyNotificationService();
        var handler = new CreateShowCommandHandler(repo, notification);

        var command = new CreateShowCommand("Testowy Tytuł", "Prezenter", new DateTime(2024, 2, 2, 15, 0, 0), 45);

        var result = await handler.Handle(command, default);

        Assert.NotNull(result);
        Assert.Equal(command.Title, result.Title);
        Assert.Equal(command.Presenter, result.Presenter);
        Assert.Equal(command.StartTime, result.StartTime);
        Assert.Equal(command.DurationMinutes, result.DurationMinutes);
    }

}
