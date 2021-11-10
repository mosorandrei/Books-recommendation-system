using MediatR;

namespace Application.Features.Commands
{
    public class DeleteBookCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public DeleteBookCommand(Guid id)
        {
            this.Id = id;
        }
    }
}
