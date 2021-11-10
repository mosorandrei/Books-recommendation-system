using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
    {
        private readonly IBookRepository repository;

        public CreateBookCommandHandler(IBookRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.Title,
                Rating = request.Rating,
                Description = request.Description,
                PublicationDate = request.PublicationDate,
                ImageUri = request.ImageUri
            };

            await repository.AddAsync(book);
            return book.Id;
        }
    }
}
