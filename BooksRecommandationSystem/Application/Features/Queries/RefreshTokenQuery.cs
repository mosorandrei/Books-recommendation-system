using Domain.AuthModels;
using MediatR;

namespace Application.Features.Queries
{
    public class RefreshTokenQuery : IRequest<RefreshTokenDto>
    {
        public string Email { get; set; }
        public RefreshTokenQuery(string email)
        {
            this.Email = email;
        }
    }
}
