using Domain.AuthModels;
using MediatR;

namespace Application.Features.Queries
{
    public class GetUserReadingStatusesQuery : IRequest<IEnumerable<UserReadingStatusesDtoFE>>
    {
        public string? UserId { get; set; }
    }
}
