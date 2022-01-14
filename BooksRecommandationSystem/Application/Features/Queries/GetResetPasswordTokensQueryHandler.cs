using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetResetPasswordTokensQueryHandler : IRequestHandler<GetResetPasswordTokensQuery, IEnumerable<ResetPasswordToken>>
    {
        private readonly IResetPasswordRepository repository;

        public GetResetPasswordTokensQueryHandler(IResetPasswordRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IEnumerable<ResetPasswordToken>> Handle(GetResetPasswordTokensQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAllAsync();
        }
    }
}
