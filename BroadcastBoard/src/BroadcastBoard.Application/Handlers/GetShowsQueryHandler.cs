using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using BroadcastBoard.Domain.Common.Interfaces;
using BroadcastBoard.Application.Shows.DTO;
using BroadcastBoard.Application.Queries;

namespace BroadcastBoard.Application.Shows.Handlers
{ 
    public class GetShowsQueryHandler : IRequestHandler<GetShowsQuery, IEnumerable<ShowDto>>
    {
        private readonly IShowRepository _repository;

        public GetShowsQueryHandler(IShowRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ShowDto>> Handle(GetShowsQuery request, CancellationToken cancellationToken)
        {
            var shows = await _repository.GetShowsByDateAsync(request.Date);

            return shows.Select(s => new ShowDto(s.Id, s.Title, s.Presenter, s.StartTime, s.DurationMinutes));
        }
    }
}

