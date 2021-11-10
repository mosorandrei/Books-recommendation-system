using MediatR;

namespace Application.Features.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
