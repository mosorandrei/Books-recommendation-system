using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetResetPasswordTokensQuery : IRequest<IEnumerable<ResetPasswordToken>>
    {
    }
}
