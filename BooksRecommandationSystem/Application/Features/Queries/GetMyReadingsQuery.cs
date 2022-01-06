using Domain.AuthModels;
using MediatR;

namespace Application.Features.Queries
{
    public class GetMyReadingsQuery : IRequest<IEnumerable<BookDtoFE>>
    {
        public string? UserId { get; set; }
    }
}
