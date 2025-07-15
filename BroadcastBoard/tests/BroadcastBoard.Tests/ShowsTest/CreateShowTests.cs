using BroadcastBoard.Domain.Entities;
using BroadcastBoard.Application.Shows.Commands;
using BroadcastBoard.Application.Common.Interfaces;
using BroadcastBoard.Domain.Common.Interfaces;
using BroadcastBoard.Application.Shows.Exceptions;
public class CreateShowTests
{
    [Fact]
    public async Task Should_Create_Show_When_No_Collision()
    {
        var repo = new FakeShowRepository(); // np. in-memory
        var notification = new FakeNotificationService();
        var handler = new CreateShowCommandHandler(repo, notification);

        var command = new CreateShowCommand("Tytuł", "Prezenter", new DateTime(2024, 1, 1, 10, 0, 0), 60);

        var result = await handler.Handle(command, default);

        Assert.NotNull(result);
        Assert.Equal("Tytuł", result.Title);
    }

    [Fact]
    public async Task Should_Throw_When_Show_Collides()
    {
        var repo = new FakeShowRepository();
        await repo.AddAsync(new Show
        {
            Id = Guid.NewGuid(),
            Title = "Istniejąca",
            Presenter = "A",
            StartTime = new DateTime(2024, 1, 1, 10, 0, 0),
            DurationMinutes = 60
        });
        var notification = new FakeNotificationService();
        var handler = new CreateShowCommandHandler(repo,notification);

        var command = new CreateShowCommand("Nowa", "B", new DateTime(2024, 1, 1, 10, 30, 0), 30);

        await Assert.ThrowsAsync<ShowCollisionException>(() => handler.Handle(command, default));
    }
    public class FakeNotificationService : INotificationService
    {
        public Task NotifyAsync(string message)
        {
            // Możesz logować, ale w testach zazwyczaj nic nie robimy
            return Task.CompletedTask;
        }
    }
    public class FakeShowRepository : IShowRepository
    {
        private readonly List<Show> _shows = new();

        public Task AddAsync(Show show)
        {
            _shows.Add(show);
            return Task.CompletedTask;
        }

        public Task<Show> GetByIdAsync(Guid id)
        {
            var show = _shows.FirstOrDefault(s => s.Id == id);
            return Task.FromResult(show);
        }

        public Task<IEnumerable<Show>> GetShowsByDateAsync(DateTime date)
        {
            var result = _shows.Where(s => s.StartTime.Date == date.Date);
            return Task.FromResult((IEnumerable<Show>)result);
        }
    }
}
