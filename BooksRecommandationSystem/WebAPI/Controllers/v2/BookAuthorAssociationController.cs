using Application.Features.Commands;
using Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}")]
    [EnableCors("FEPolicy")]
    public class BookAuthorAssociationController : BaseController
    {
        public BookAuthorAssociationController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Roles = "Member, Administrator")]
        [HttpGet("GetBooksByAuthorId")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetBooksByAuthorId(Guid AuthorId)
        {
            return Ok(await mediator.Send(new GetBooksByAuthorIdQuery()
            {
                AuthorId = AuthorId
            }));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("AddAuthorToBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<string> AddAuthorToBook(Guid BookId, Guid AuthorId)
        {
            AddAuthorToBookCommand command = new()
            {
                BookId = BookId,
                AuthorId = AuthorId
            };
            return await mediator.Send(command);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("DeleteAuthorFromBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<string> DeleteAuthorFromBook(Guid BookId, Guid AuthorId)
        {
            DeleteAuthorFromBookCommand command = new()
            {
                BookId = BookId,
                AuthorId = AuthorId
            };
            return await mediator.Send(command);
        }
    }
}
