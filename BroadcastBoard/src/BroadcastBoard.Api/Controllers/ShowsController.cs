using BroadcastBoard.Application.Queries;
using BroadcastBoard.Application.Shows.Commands;
using BroadcastBoard.Application.Shows.Exceptions;
using BroadcastBoard.Application.Shows.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BroadcastBoard.Api.Controllers
{
    [ApiController]
    [Route("api/shows")]
    public class ShowsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShowsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateShowCommand command)
        {

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

        }

        [HttpGet]
        public async Task<IActionResult> GetByDate([FromQuery] DateTime date)
        {
            var shows = await _mediator.Send(new GetShowsQuery(date.Date));
            return Ok(shows);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var show = await _mediator.Send(new GetShowByIdQuery(id));
            return Ok(show);
        }
    }
}
