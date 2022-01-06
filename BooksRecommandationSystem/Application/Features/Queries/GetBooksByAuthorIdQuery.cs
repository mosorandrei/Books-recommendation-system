using Domain.AuthModels;
using MediatR;

namespace Application.Features.Queries
{
    public class GetBooksByAuthorIdQuery : IRequest<IEnumerable<BookDtoFE>>
    {
        public Guid AuthorId { get; set; }
    }
}
