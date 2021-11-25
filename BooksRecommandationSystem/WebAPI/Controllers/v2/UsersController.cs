using Application.Features.Commands;
using Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : BaseController
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await mediator.Send(new GetUsersQuery()));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserCommand command)
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
            return Ok(await mediator.Send(new DeleteUserCommand(id)));
        }
    }
}
