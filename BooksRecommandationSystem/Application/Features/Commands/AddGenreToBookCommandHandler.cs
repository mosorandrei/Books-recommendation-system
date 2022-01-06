using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands
{
    public class AddGenreToBookCommandHandler : IRequestHandler<AddGenreToBookCommand, string>
    {
        private readonly IBookGenreAssociationRepository repository;
        private readonly IBookRepository bookRepository;
        private readonly IGenreRepository genreRepository;

        public AddGenreToBookCommandHandler(IBookGenreAssociationRepository repository, IBookRepository bookRepository, IGenreRepository genreRepository)
        {
            this.repository = repository;
            this.bookRepository = bookRepository;
            this.genreRepository = genreRepository;
        }

        public async Task<string> Handle(AddGenreToBookCommand request, CancellationToken cancellationToken)
        {
            if (bookRepository.GetByIdAsync(request.BookId).Result == null)
            {
                throw new ArgumentNullException("No book found!");
            }
            if (genreRepository.GetByIdAsync(request.GenreId).Result == null)
            {
                throw new ArgumentNullException("No genre found!");
            }
            IEnumerable<BookGenreAssociation> associations = await repository.GetAllAsync();
            foreach (BookGenreAssociation association in associations) {
                if (association.BookId == request.BookId && association.GenreId == request.GenreId) {
                    return "Genre already associated with book!";
                }
            }
            await repository.AddAsync(new BookGenreAssociation
            {
                GenreId = request.GenreId,
                BookId = request.BookId
            });
            return "Genre associated with book successfully!";
        }
    }
}
