using Application.Interfaces;
using Domain.AuthModels;
using MediatR;

namespace Application.Features.Queries
{
    public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, RefreshTokenDto>
    {
        private readonly ITokenRepository repository;
        public RefreshTokenQueryHandler(ITokenRepository repository)
        {
            this.repository = repository;
        }

        public async Task<RefreshTokenDto> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            return await repository.RefreshToken(request.Email);
        }
    }
}
