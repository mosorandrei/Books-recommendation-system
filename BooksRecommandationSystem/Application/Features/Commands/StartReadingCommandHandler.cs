using Application.Features.Responses;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Commands
{
    public class StartReadingCommandHandler : IRequestHandler<StartReadingCommand, BookStatusChangeResponse>
    {
        private readonly IReadingStatusRepository repository;

        public StartReadingCommandHandler(IReadingStatusRepository repository)
        {
            this.repository = repository;
        }

        public async Task<BookStatusChangeResponse> Handle(StartReadingCommand command, CancellationToken cancellationToken)
        {
            if (command.UserId == null)
            {
                throw new ArgumentNullException(command.UserId);
            }

            BookStatusChangeResponse response = new();
            response.Resource = new Domain.AuthModels.StatusResponse
            {
                UserId = command.UserId,
                BookId = command.BookId,
                Status = await repository.StartReading(command.UserId, command.BookId)
            };
            return response;
        }
    }
}
