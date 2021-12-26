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
    public class BookGenreAssociationController : BaseController
    {
        public BookGenreAssociationController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Roles = "Member, Administrator")]
        [HttpGet("GetBooksByGenreId")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetBooksByGenreId(Guid GenreId)
        {
            return Ok(await mediator.Send(new GetBooksByGenreIdQuery()
            {
                GenreId = GenreId
            }));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("AddGenreToBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<string> AddGenreToBook(Guid BookId, Guid GenreId)
        {
            AddGenreToBookCommand command = new()
            {
                BookId = BookId,
                GenreId = GenreId
            };
            return await mediator.Send(command);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("DeleteGenreFromBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<string> DeleteGenreFromBook(Guid BookId, Guid GenreId)
        {
            DeleteGenreFromBookCommand command = new()
            {
                BookId = BookId,
                GenreId = GenreId
            };
            return await mediator.Send(command);
        }
    }
}
