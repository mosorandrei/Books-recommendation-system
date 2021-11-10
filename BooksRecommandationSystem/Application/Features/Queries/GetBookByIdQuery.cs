using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public Guid Id { get; set; }
    }
}
