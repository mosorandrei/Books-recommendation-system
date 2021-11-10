using MediatR;

namespace Application.Features.Commands
{
    public class DeleteAuthorCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public DeleteAuthorCommand(Guid id)
        {
            this.Id = id;
        }
    }
}
