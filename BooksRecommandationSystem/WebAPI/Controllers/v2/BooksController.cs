﻿using Application.Features.Commands;
using Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [EnableCors("FEPolicy")]
    public class BooksController : BaseController
    {
        public BooksController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Roles = "Member,Administrator")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await mediator.Send(new GetBooksQuery()));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            return Ok(await mediator.Send(command));
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await mediator.Send(new DeleteBookCommand(id)));
        }
    }
}
