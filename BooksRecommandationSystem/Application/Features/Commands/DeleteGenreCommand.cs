using MediatR;

namespace Application.Features.Commands
{
    public class DeleteGenreCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public DeleteGenreCommand(Guid id)
        {
            this.Id = id;
        }
    }
}
