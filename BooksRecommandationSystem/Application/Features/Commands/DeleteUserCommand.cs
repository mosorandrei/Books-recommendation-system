using MediatR;

namespace Application.Features.Commands
{
    public class DeleteUserCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
