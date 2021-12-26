using Domain.AuthModels;
using MediatR;

namespace Application.Features.Queries
{
    public class GetBooksQuery : IRequest<IEnumerable<BookDtoFE>>
    {
    }
}