using MediatR;

namespace Application.Features.Commands
{
    public class BlockUserCommand : IRequest<string>
    {
        public string? UserId { get; set; }
    }
}
