using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetGenreByIdQuery : IRequest<Genre>
    {
        public Guid Id { get; set; }
    }
}
