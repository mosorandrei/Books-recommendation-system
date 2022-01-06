using Application.Features.Responses;
using Application.Interfaces;
using Domain.AuthModels;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands
{
    public class RateBookCommandHandler : IRequestHandler<RateBookCommand, BookStatusChangeResponse>
    {
        private readonly IReadingStatusRepository repository;
        private readonly IBookRepository bookRepository;

        public RateBookCommandHandler(IReadingStatusRepository repository, IBookRepository bookRepository)
        {
            this.repository = repository;
            this.bookRepository = bookRepository;
        }

        public async Task<BookStatusChangeResponse> Handle(RateBookCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
            {
                throw new ArgumentNullException(request.UserId);
            }

            BookStatusChangeResponse response = new();
            response.Resource = new StatusResponse
            {
                UserId = request.UserId,
                BookId = request.BookId
            };

            IEnumerable<ReadingStatus> readingStatuses = await repository.GetAllAsync();
            foreach (ReadingStatus status in readingStatuses)
            {
                if (status.ApplicationUserId == request.UserId && status.BookId == request.BookId)
                {
                    if (status.Status != Domain.Constants.ReadingStatusEnum.Read)
                    {
                        response.Resource.Status = "The Rating cannot be made since the Book has not been read yet!";
                        return response;
                    }
                    await bookRepository.UpdateBookReviews(request.BookId, status.UserScore, request.Score);
                    await repository.RateBook(request.UserId, request.BookId, request.Score);
                    response.Resource.Status = "The Rating has been added successfully!";
                    return response;
                }
            }
            throw new NullReferenceException("No Entry found with the specified parameters!");
        }
    }
}
