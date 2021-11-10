using MediatR;

namespace Application.Features.Commands
{
    public class CreateGenreCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }
}
