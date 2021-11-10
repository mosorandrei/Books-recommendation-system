using MediatR;

namespace Application.Features.Commands
{
    public class DeleteGenreCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
