using Domain.AuthModels;
using MediatR;

namespace Application.Features.Queries
{
    public class GetMembersQuery : IRequest<IEnumerable<ApplicationUserDto>>
    {
    }
}
