using Application.Features.Responses;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Commands
{

    public class AddToMyFavouritesCommandHandler : IRequestHandler<AddToMyFavouritesCommand, AddOrDeleteFavouritesResponse>
    {
        private readonly IReadingStatusRepository repository;

        public AddToMyFavouritesCommandHandler(IReadingStatusRepository repository)
        {
            this.repository = repository;
        }

        public async Task<AddOrDeleteFavouritesResponse> Handle(AddToMyFavouritesCommand command, CancellationToken cancellationToken)
        {
            if (command.UserId == null)
            {
                throw new ArgumentNullException(command.UserId);
            }

            AddOrDeleteFavouritesResponse response = new();
            response.Resource = new Domain.AuthModels.FavouritesResponse
            {
                UserId = command.UserId,
                BookId = command.BookId,
                Status = await repository.AddToMyFavourites(command.UserId, command.BookId)
            };
            return response;
        }
    }
}
