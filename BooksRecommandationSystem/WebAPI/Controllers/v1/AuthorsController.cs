using Application.Features.Commands;
using Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class AuthorsController : BaseController
    {
        public AuthorsController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuthorCommand command)
        {
            return Ok(await mediator.Send(command));
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await mediator.Send(new GetAuthorsQuery()));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAuthorCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            return Ok(await mediator.Send(command));
        }
    }
}
