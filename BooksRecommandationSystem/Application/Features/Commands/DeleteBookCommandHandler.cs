using Application.Interfaces;
using MediatR;

namespace Application.Features.Commands
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Guid>
    {
        private readonly IBookRepository repository;

        public DeleteBookCommandHandler(IBookRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Guid> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = repository.GetByIdAsync(request.Id).Result;
            if (book == null)
            {
                throw new Exception("Book does not exist!");
            }
            await repository.DeleteAsync(book);
            return book.Id;
        }
    }
}
