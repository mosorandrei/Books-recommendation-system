using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetStatusesQuery : IRequest<IEnumerable<ReadingStatus>>
    {
    }
}
