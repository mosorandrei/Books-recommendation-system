using Application.Interfaces;
using Domain.AuthModels;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetBooksByGenreIdQueryHandler : IRequestHandler<GetBooksByGenreIdQuery, IEnumerable<BookDtoFE>>
    {
        private readonly IBookGenreAssociationRepository repository;
        private readonly IGenreRepository genreRepository;
        private readonly IBookRepository bookRepository;
        private readonly IBookAuthorAssociationRepository bookAuthorAssociationRepository;
        private readonly IAuthorRepository authorRepository;

        public GetBooksByGenreIdQueryHandler(IBookGenreAssociationRepository repository, IBookRepository bookRepository, IBookAuthorAssociationRepository bookAuthorAssociationRepository, IAuthorRepository authorRepository, IGenreRepository genreRepository)
        {
            this.repository = repository;
            this.bookRepository = bookRepository;
            this.bookAuthorAssociationRepository = bookAuthorAssociationRepository;
            this.authorRepository = authorRepository;
            this.genreRepository = genreRepository;
        }

        public async Task<IEnumerable<BookDtoFE>> Handle(GetBooksByGenreIdQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Guid> bookIds = await repository.GetBooksByGenreId(request.GenreId);
            List<Book> books = new();
            foreach (Guid bookId in bookIds)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                Book fetchedBook = await bookRepository.GetByIdAsync(bookId);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                if (fetchedBook == null)
                {
                    throw new InvalidDataException("Book does not exist!");
                }
                books.Add(fetchedBook);
            }
            List<BookDtoFE> bookDtos = new();
            foreach (Book fetchedBook in books)
            {
                var bookDto = new BookDtoFE
                {
                    BookId = fetchedBook.Id,
                    Title = fetchedBook.Title,
                    Rating = fetchedBook.Rating,
                    Description = fetchedBook.Description,
                    DownloadUri = fetchedBook.DownloadUri,
                    PublicationDate = fetchedBook.PublicationDate,
                    UploadDate = fetchedBook.UploadDate,
                    ImageUri = fetchedBook.ImageUri
                };
                ICollection<Guid> genreIds = await repository.GetGenresByBookId(fetchedBook.Id);
                ICollection<Genre> genres = new List<Genre>();
                foreach (Guid genreId in genreIds)
                {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                    Genre genre = await genreRepository.GetByIdAsync(genreId);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                    if (genre == null)
                        throw new InvalidDataException(nameof(genre));
                    genres.Add(genre);
                }
                bookDto.Genres = genres;
                ICollection<Guid> authorIds = await bookAuthorAssociationRepository.GetAuthorsByBookId(fetchedBook.Id);
                ICollection<Author> authors = new List<Author>();
                foreach (Guid authorId in authorIds)
                {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                    Author author = await authorRepository.GetByIdAsync(authorId);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                    if (author == null)
                        throw new InvalidDataException(nameof(author));
                    authors.Add(author);
                }
                bookDto.Authors = authors;

                bookDtos.Add(bookDto);
            }
            return bookDtos;
        }
    }
}
