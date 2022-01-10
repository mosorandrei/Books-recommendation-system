using Application.Interfaces;
using MediatR;

namespace Application.Features.Commands
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Guid>
    {
        private readonly IBookRepository repository;

        public UpdateBookCommandHandler(IBookRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Guid> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = repository.GetByIdAsync(request.Id).Result;
            if (book == null || book.Id == Guid.Empty)
            {
                throw new InvalidDataException("Book does not exist!");
            }
            book.Description = request.Description;
            book.Title = request.Title;
            book.PublicationDate = request.PublicationDate;
            book.UploadDate = request.UploadDate;
            book.ImageUri = request.ImageUri;
            book.DownloadUri = request.DownloadUri;

            await repository.UpdateAsync(book);

            return book.Id;
        }
    }
}
