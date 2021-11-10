using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetAuthorsQuery : IRequest<IEnumerable<Author>>
    {
    }
}
