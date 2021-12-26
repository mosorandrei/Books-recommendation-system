using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetMyReadsQuery : IRequest<IEnumerable<Book>>
    {
        public string? UserId { get; set; }
    }
}
