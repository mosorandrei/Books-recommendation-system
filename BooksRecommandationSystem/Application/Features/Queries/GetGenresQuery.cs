using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetGenresQuery : IRequest<IEnumerable<Genre>>
    {
    }
}
