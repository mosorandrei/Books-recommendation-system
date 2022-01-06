using Application.Features.Responses;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Commands
{

    public class DeleteFromMyFavouritesCommandHandler : IRequestHandler<DeleteFromMyFavouritesCommand, AddOrDeleteFavouritesResponse>
    {
        private readonly IReadingStatusRepository repository;

        public DeleteFromMyFavouritesCommandHandler(IReadingStatusRepository repository)
        {
            this.repository = repository;
        }

        public async Task<AddOrDeleteFavouritesResponse> Handle(DeleteFromMyFavouritesCommand command, CancellationToken cancellationToken)
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
                Status = await repository.DeleteFromMyFavourites(command.UserId, command.BookId)
            };
            return response;
        }
    }
}

