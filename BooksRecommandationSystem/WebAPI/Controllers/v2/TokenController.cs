using Application.Features.Commands;
using Domain.AuthModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TokenController : BaseController
    {
        public TokenController(IMediator mediator) : base(mediator)
        {
        }
        // POST: api/Token/Authenticate
        /// <summary>
        ///     Validate that the user account is valid and return an auth token
        ///     to the requesting app for use in the api.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<TokenResponse> AuthenticateAsync([FromBody] AuthenticateCommand command)
        {
            var response = await mediator.Send(command);
            return response.Resource;
        }
    }
}
