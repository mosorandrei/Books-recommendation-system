using Domain.AuthModels;
using MediatR;

namespace Application.Features.Queries
{
    public class GetMyFavouritesQuery : IRequest<IEnumerable<BookDtoFE>>
    {
        public string? UserId { get; set; }
    }
}
