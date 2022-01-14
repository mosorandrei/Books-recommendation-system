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
    public class ResetPasswordController : BaseController
    {
        public ResetPasswordController(IMediator mediator) : base(mediator)
        {
        }

        [AllowAnonymous]
        [HttpPost("SendResetEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> SendResetEmail(string Email)
        {
            return Ok(await mediator.Send(new SendResetEmailCommand(Email)));
        }

        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> ResetPassword(string Email, int Token, string Password, string ConfirmPassword)
        {
            return Ok(await mediator.Send(new ResetPasswordCommand(Email, Token, Password, ConfirmPassword)));
        }


        [Authorize(Roles = "Member,Administrator")]
        [HttpGet("GetAllResetTokens")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetAllResetTokens()
        {
            return Ok(await mediator.Send(new GetResetPasswordTokensQuery()));
        }

    }
}
