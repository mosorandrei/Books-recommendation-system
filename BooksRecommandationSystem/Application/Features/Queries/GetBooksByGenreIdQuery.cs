using Domain.AuthModels;
using MediatR;

namespace Application.Features.Queries
{
    public class GetBooksByGenreIdQuery : IRequest<IEnumerable<BookDtoFE>>
    {
        public Guid GenreId { get; set; }
    }
}
