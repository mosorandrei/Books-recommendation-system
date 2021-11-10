using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetAuthorByIdQuery : IRequest<Author>
    {
        public Guid Id { get; set; }
    }
}
