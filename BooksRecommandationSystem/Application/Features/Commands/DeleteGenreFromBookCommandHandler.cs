using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands
{
    public class DeleteGenreFromBookCommandHandler : IRequestHandler<DeleteGenreFromBookCommand, string>
    {
        private readonly IBookGenreAssociationRepository repository;
        private readonly IBookRepository bookRepository;
        private readonly IGenreRepository genreRepository;

        public DeleteGenreFromBookCommandHandler(IBookGenreAssociationRepository repository, IBookRepository bookRepository, IGenreRepository genreRepository)
        {
            this.repository = repository;
            this.bookRepository = bookRepository;
            this.genreRepository = genreRepository;
        }

        public async Task<string> Handle(DeleteGenreFromBookCommand request, CancellationToken cancellationToken)
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
            foreach (BookGenreAssociation association in associations)
            {
                if (association.BookId == request.BookId && association.GenreId == request.GenreId)
                {
                    await repository.DeleteAsync(association);
                    return "Genre no longer associated with book!";
                }
            }
            return "Genre is not associated with book!";
        }
    }
}
