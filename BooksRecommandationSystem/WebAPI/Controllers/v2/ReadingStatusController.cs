using Application.Features.Commands;
using Application.Features.Queries;
using Application.Features.Responses;
using Domain.AuthModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}")]
    [EnableCors("FEPolicy")]
    public class ReadingStatusController : BaseController
    {
        public ReadingStatusController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("GetAllStatuses")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetAllStatuses()
        {
            return Ok(await mediator.Send(new GetStatusesQuery()));
        }

        [Authorize(Roles = "Member, Administrator")]
        [HttpPost("AddToMyFavourites")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<FavouritesResponse> AddToMyFavourites(Guid BookId)
        {
            AddToMyFavouritesCommand command = new()
            {
                BookId = BookId,
                UserId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value
            };
            AddOrDeleteFavouritesResponse response = await mediator.Send(command);
            return response.Resource is not null ? response.Resource : throw new ArgumentNullException(nameof(command));
        }

        [Authorize(Roles = "Member, Administrator")]
        [HttpDelete("DeleteFromMyFavourites")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<FavouritesResponse> DeleteFromMyFavourites(Guid BookId)
        {
            DeleteFromMyFavouritesCommand command = new()
            {
                BookId = BookId,
                UserId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value
            };
            AddOrDeleteFavouritesResponse response = await mediator.Send(command);
            return response.Resource is not null ? response.Resource : throw new ArgumentNullException(nameof(command));
        }

        [Authorize(Roles = "Member, Administrator")]
        [HttpGet("GetMyFavourites")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetMyFavourites()
        {
            return Ok(await mediator.Send(new GetMyFavouritesQuery()
            {
                UserId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value
            }));
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("GetUserFavourites")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetUserFavourites(string UserId)
        {
            return Ok(await mediator.Send(new GetMyFavouritesQuery()
            {
                UserId = UserId
            }));
        }

        [Authorize(Roles = "Member, Administrator")]
        [HttpGet("GetMyToReads")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetMyToReads()
        {
            return Ok(await mediator.Send(new GetMyToReadsQuery()
            {
                UserId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value
            }));
        }

        [Authorize(Roles = "Member, Administrator")]
        [HttpPut("StartReading")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<StatusResponse> StartReading(Guid BookId)
        {
            StartReadingCommand command = new()
            {
                BookId = BookId,
                UserId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value
            };
            BookStatusChangeResponse response = await mediator.Send(command);
            return response.Resource is not null ? response.Resource : throw new ArgumentNullException(nameof(command));
        }

        [Authorize(Roles = "Member, Administrator")]
        [HttpGet("GetMyReadings")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetMyReadings()
        {
            return Ok(await mediator.Send(new GetMyReadingsQuery()
            {
                UserId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value
            }));
        }

        [Authorize(Roles = "Member, Administrator")]
        [HttpPut("FinishReading")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<StatusResponse> FinishReading(Guid BookId)
        {
            FinishReadingCommand command = new()
            {
                BookId = BookId,
                UserId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value
            };
            BookStatusChangeResponse response = await mediator.Send(command);
            return response.Resource is not null ? response.Resource : throw new ArgumentNullException(nameof(command));
        }

        [Authorize(Roles = "Member, Administrator")]
        [HttpGet("GetMyReads")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetMyReads()
        {
            return Ok(await mediator.Send(new GetMyReadsQuery()
            {
                UserId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value
            }));
        }
    }
}
