using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetMyToReadsQuery : IRequest<IEnumerable<Book>>
    {
        public string? UserId { get; set; }
    }
}
