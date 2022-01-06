using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands
{
    public class AddAuthorToBookCommandHandler : IRequestHandler<AddAuthorToBookCommand, string>
    {
        private readonly IBookAuthorAssociationRepository repository;
        private readonly IBookRepository bookRepository;
        private readonly IAuthorRepository authorRepository;

        public AddAuthorToBookCommandHandler(IBookAuthorAssociationRepository repository, IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            this.repository = repository;
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
        }

        public async Task<string> Handle(AddAuthorToBookCommand request, CancellationToken cancellationToken)
        {
            if (bookRepository.GetByIdAsync(request.BookId).Result == null)
            {
                throw new ArgumentNullException("No book found!");
            }
            if (authorRepository.GetByIdAsync(request.AuthorId).Result == null)
            {
                throw new ArgumentNullException("No genre found!");
            }
            IEnumerable<BookAuthorAssociation> associations = await repository.GetAllAsync();
            foreach (BookAuthorAssociation association in associations)
            {
                if (association.BookId == request.BookId && association.AuthorId == request.AuthorId)
                {
                    return "Author already associated with book!";
                }
            }
            await repository.AddAsync(new BookAuthorAssociation
            {
                AuthorId = request.AuthorId,
                BookId = request.BookId
            });
            return "Author associated with book successfully!";
        }
    }
}
