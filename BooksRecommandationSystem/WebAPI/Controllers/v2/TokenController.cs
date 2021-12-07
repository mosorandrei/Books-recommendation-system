﻿using Application.Features.Commands;
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
    [Route("api/v{version:apiVersion}/[controller]")]
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
            Console.WriteLine(command);
            var response = await mediator.Send(command);
            return response.Resource is not null ? response.Resource : throw new ArgumentNullException(nameof(command));
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

        [AllowAnonymous]
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
        public IActionResult GetUser()
        {
            var currentUser = new ApplicationUserDtoFE
            {
                UserId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value,
                FullName = User.Claims.FirstOrDefault(x => x.Type == "FullName")?.Value,
                Email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
                IsAdmin = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value == "Administrator" ? 1 : 0
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
            if(email == null)
                throw new ArgumentNullException("Email is null for the provided token!");
            return Ok(await mediator.Send(new RefreshTokenQuery(email)));
        }

        /*
        // GET: api/Token/GetAllAdmins
        /// <summary>
        ///     Get all Admins in Database
        /// </summary>
        */

        [AllowAnonymous]
        [HttpGet("GetAllAdmins")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetAdmins()
        {
            return Ok(await mediator.Send(new GetAdminsQuery()));
        }
    }
}
