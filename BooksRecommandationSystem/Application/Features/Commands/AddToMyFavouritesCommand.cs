using Application.Features.Responses;
using Domain.AuthModels;
using MediatR;

namespace Application.Features.Commands
{
    public class AddToMyFavouritesCommand : FavouritesRequest, IRequest<AddOrDeleteFavouritesResponse>
    {
    }
}
