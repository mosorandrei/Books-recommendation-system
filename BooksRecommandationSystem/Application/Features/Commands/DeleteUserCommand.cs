using MediatR;

namespace Application.Features.Commands
{
    public class DeleteUserCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public DeleteUserCommand(Guid id)
        {
            this.Id = id;
        }
    }
}
