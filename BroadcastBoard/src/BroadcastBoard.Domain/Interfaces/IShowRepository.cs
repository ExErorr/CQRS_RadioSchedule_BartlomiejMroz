using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BroadcastBoard.Domain.Entities;

namespace BroadcastBoard.Domain.Common.Interfaces
{
    public interface IShowRepository
    {
        Task<IEnumerable<Show>> GetShowsByDateAsync(DateTime date);
        Task<Show> GetByIdAsync(Guid id);
        Task AddAsync(Show show);
    }

}
