using Domain.AuthModels;
using MediatR;

namespace Application.Features.Queries
{
    public class GetMyReadsQuery : IRequest<IEnumerable<ReadBookDtoFE>>
    {
        public string? UserId { get; set; }
    }
}
