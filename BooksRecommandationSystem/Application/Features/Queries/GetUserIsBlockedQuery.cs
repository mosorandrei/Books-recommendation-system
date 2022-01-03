using MediatR;

namespace Application.Features.Queries
{
    public class GetUserIsBlockedQuery : IRequest<int>
    {
        public string? UserId { get; set; }
    }
}
