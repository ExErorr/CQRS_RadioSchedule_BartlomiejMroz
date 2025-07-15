using BroadcastBoard.Domain.Entities;
using BroadcastBoard.Domain.Common.Interfaces;

namespace BroadcastBoard.Infrastructure.Repositories;
public class InMemoryShowRepository : IShowRepository
{
    private readonly List<Show> _shows = new();

    public Task AddAsync(Show show)
    {
        _shows.Add(show);
        return Task.CompletedTask;
    }

    public Task<Show> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_shows.FirstOrDefault(s => s.Id == id));
    }

    public Task<IEnumerable<Show>> GetShowsByDateAsync(DateTime date)
    {
        var results = _shows
            .Where(s => s.StartTime.Date == date.Date)
            .AsEnumerable();

        return Task.FromResult(results);
    }
}
