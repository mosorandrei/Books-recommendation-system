using Application.Features.Responses;
using Domain.AuthModels;
using MediatR;

namespace Application.Features.Commands
{
    public class RegisterMemberCommand : RegisterRequest, IRequest<RegistrationResponse>
    {
    }
}
