using MediatR;

namespace Application.Features.Commands
{
    public class UpdateGenreCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
