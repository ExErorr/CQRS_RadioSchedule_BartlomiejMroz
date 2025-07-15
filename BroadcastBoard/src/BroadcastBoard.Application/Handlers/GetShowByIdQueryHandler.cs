using BroadcastBoard.Application.Shows.DTO;
using BroadcastBoard.Application.Shows.Queries;
using BroadcastBoard.Domain.Common.Interfaces;
using MediatR;

namespace BroadcastBoard.Application.Shows.Handlers
{
    public class GetShowByIdQueryHandler : IRequestHandler<GetShowByIdQuery, ShowDto>
    {
        private readonly IShowRepository _repository;

        public GetShowByIdQueryHandler(IShowRepository repository)
        {
            _repository = repository;
        }

        public async Task<ShowDto> Handle(GetShowByIdQuery request, CancellationToken cancellationToken)
        {
            var show = await _repository.GetByIdAsync(request.Id);

            if (show == null)
                return null;

            return new ShowDto(show.Id, show.Title, show.Presenter, show.StartTime, show.DurationMinutes);
        }
    }
}

