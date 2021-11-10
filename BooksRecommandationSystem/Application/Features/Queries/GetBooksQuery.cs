using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetBooksQuery : IRequest<IEnumerable<Book>>
    {
    }
}
