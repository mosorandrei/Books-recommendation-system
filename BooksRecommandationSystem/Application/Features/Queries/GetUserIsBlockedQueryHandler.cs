using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetUserIsBlockedQueryHandler : IRequestHandler<GetUserIsBlockedQuery, int>
    {
        private readonly ITokenRepository repository;

        public GetUserIsBlockedQueryHandler(ITokenRepository repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(GetUserIsBlockedQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
                throw new ArgumentNullException(nameof(request.UserId));
            ApplicationUser user = await repository.GetUserById(request.UserId);
            return user.IsBlocked;
        }
    }
}
