using Application.Interfaces;
using Domain.AuthModels;
using MediatR;

namespace Application.Features.Queries
{
    public class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, IEnumerable<ApplicationUserDto>>
    {
        private readonly ITokenRepository repository;

        public GetMembersQueryHandler(ITokenRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IEnumerable<ApplicationUserDto>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAllMembersAsync();
        }
    }
}
