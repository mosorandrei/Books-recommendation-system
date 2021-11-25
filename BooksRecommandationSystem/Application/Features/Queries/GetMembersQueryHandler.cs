using Application.Interfaces;
using Domain.AuthModels;
using MediatR;

namespace Application.Features.Queries
{
    public class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, IEnumerable<ApplicationUserDTO>>
    {
        private readonly ITokenRepository repository;

        public GetMembersQueryHandler(ITokenRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IEnumerable<ApplicationUserDTO>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAllMembersAsync();
        }
    }
}
