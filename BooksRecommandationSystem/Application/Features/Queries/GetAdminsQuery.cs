using Domain.AuthModels;
using MediatR;

namespace Application.Features.Queries
{
    public class GetAdminsQuery : IRequest<IEnumerable<ApplicationUserDTO>>
    {
    }
}
