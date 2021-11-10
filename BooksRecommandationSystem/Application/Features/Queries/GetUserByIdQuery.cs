using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public Guid Id { get; set; }
    }
}
