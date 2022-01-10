using MediatR;

namespace Application.Features.Commands
{
    public class UnblockUserCommand : IRequest<string>
    {
        public string? UserId { get; set; }
    }
}
