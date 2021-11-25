using Application.Features.Commands;
using Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GenresController : BaseController
    {
        public GenresController(IMediator mediator) : base(mediator)
        {
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await mediator.Send(new GetGenresQuery()));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGenreCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGenreCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            return Ok(await mediator.Send(command));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await mediator.Send(new DeleteGenreCommand(id)));
        }
    }
}
