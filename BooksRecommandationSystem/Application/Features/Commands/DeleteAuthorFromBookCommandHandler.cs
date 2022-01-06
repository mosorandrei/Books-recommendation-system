using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands
{
    public class DeleteAuthorFromBookCommandHandler : IRequestHandler<DeleteAuthorFromBookCommand, string>
    {
        private readonly IBookAuthorAssociationRepository repository;
        private readonly IBookRepository bookRepository;
        private readonly IAuthorRepository authorRepository;

        public DeleteAuthorFromBookCommandHandler(IBookAuthorAssociationRepository repository, IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            this.repository = repository;
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
        }

        public async Task<string> Handle(DeleteAuthorFromBookCommand request, CancellationToken cancellationToken)
        {
            if (bookRepository.GetByIdAsync(request.BookId).Result == null)
            {
                throw new ArgumentNullException("No book found!");
            }
            if (authorRepository.GetByIdAsync(request.AuthorId).Result == null)
            {
                throw new ArgumentNullException("No author found!");
            }
            IEnumerable<BookAuthorAssociation> associations = await repository.GetAllAsync();
            foreach (BookAuthorAssociation association in associations)
            {
                if (association.BookId == request.BookId && association.AuthorId == request.AuthorId)
                {
                    await repository.DeleteAsync(association);
                    return "Author no longer associated with book!";
                }
            }
            return "Author is not associated with book!";
        }
    }
}
