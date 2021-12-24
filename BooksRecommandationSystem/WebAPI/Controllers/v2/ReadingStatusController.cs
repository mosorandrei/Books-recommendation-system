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
    public class ReadingStatusController : BaseController
    {
        public ReadingStatusController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Roles = "Member,Administrator")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await mediator.Send(new GetStatusesQuery()));
        }
    }
}
