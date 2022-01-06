using Application.Features.Commands;
using Application.Features.Queries;
using Domain.AuthModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}")]
    [EnableCors("FEPolicy")]
    public class TokenController : BaseController
    {
        public TokenController(IMediator mediator) : base(mediator)
        {
        }

        /*
        // POST: api/Token/Authenticate
        /// <summary>
        ///     Validate that the user account is valid and return an auth token
        ///     to the requesting app for use in the api.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        */

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<TokenResponse> AuthenticateAsync([FromBody] AuthenticateCommand command)
        {
            var response = await mediator.Send(command);
            return response.Resource ?? throw new ArgumentNullException(nameof(command));
        }

        /*
        // POST: api/Token/RegisterAsync
        /// <summary>
        ///     Register a new User in the Database
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        */

        [AllowAnonymous]
        [HttpPost("RegisterMember")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<RegisterResponse> RegisterAsync([FromBody] RegisterMemberCommand command)
        {
            var response = await mediator.Send(command);
            return response.Resource is not null ? response.Resource : throw new ArgumentNullException(nameof(command));
        }

        /*
        // GET: api/Token/GetMembers
        /// <summary>
        ///     Get all Members in Database
        /// </summary>
        */

        [Authorize(Roles = "Administrator")]
        [HttpGet("GetAllMembers")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetMembers()
        {
            return Ok(await mediator.Send(new GetMembersQuery()));
        }

        /*
        // GET: api/Token/GetUser
        /// <summary>
        ///     Get specific member based on its token
        /// </summary>
        */

        [Authorize]
        [HttpGet("GetUser")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetUser()
        {
            var UserIdTemp = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
            IEnumerable<BookDtoFE> userReadings = await mediator.Send(new GetMyReadingsQuery()
            {
                UserId = UserIdTemp
            });
            IEnumerable<ReadBookDtoFE> userReads = await mediator.Send(new GetMyReadsQuery()
            {
                UserId = UserIdTemp
            });
            var currentUser = new ApplicationUserDtoFE
            {
                UserId = UserIdTemp,
                FullName = User.Claims.FirstOrDefault(x => x.Type == "FullName")?.Value,
                ImageUri = await mediator.Send(new GetUserImageUriQuery()
                {
                    UserId = UserIdTemp
                }),
                Email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
                IsAdmin = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value == "Administrator" ? 1 : 0,
                IsBlocked = await mediator.Send(new GetUserIsBlockedQuery()
                {
                    UserId = UserIdTemp
                }),
                NumberOfReadings = userReadings.Count(),
                NumberOfReads = userReads.Count()
            };
            return Ok(currentUser);
        }

        /*
        // GET: api/Token/RefreshToken
        /// <summary>
        ///     Refresh a token
        /// </summary>
        */

        [Authorize]
        [HttpGet("RefreshToken")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            string? email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            if (email is null)
            {
                throw new ArgumentNullException(email);
            }

            return Ok(await mediator.Send(new RefreshTokenQuery(email)));
        }

        /*
        // GET: api/Token/GetAllAdmins
        /// <summary>
        ///     Get all Admins in Database
        /// </summary>
        */

        [Authorize(Roles = "Administrator")]
        [HttpGet("GetAllAdmins")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetAdmins()
        {
            return Ok(await mediator.Send(new GetAdminsQuery()));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("BlockUser")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> BlockUser(string UserId)
        {
            return Ok(await mediator.Send(new BlockUserCommand()
            {
                UserId = UserId
            }));
        }
    }
}
