using MediatR;

namespace Application.Features.Commands
{
    public class DeleteGenreFromBookCommand : IRequest<string>
    {
        public Guid BookId { get; set; }
        public Guid GenreId { get; set; }
    }
}
