using MediatR;

namespace Application.Features.Commands
{
    public class CreateAuthorCommand : IRequest<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
