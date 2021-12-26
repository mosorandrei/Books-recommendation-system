using Domain.AuthModels;
using MediatR;

namespace Application.Features.Queries
{
    public class GetBookByIdQuery : IRequest<BookDtoFE>
    {
        public Guid Id { get; set; }
    }
}
