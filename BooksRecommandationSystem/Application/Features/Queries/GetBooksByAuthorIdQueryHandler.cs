using Application.Interfaces;
using Domain.AuthModels;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetBooksByAuthorIdQueryHandler : IRequestHandler<GetBooksByAuthorIdQuery, IEnumerable<BookDtoFE>>
    {
        private readonly IBookGenreAssociationRepository genreAssociationRepository;
        private readonly IGenreRepository genreRepository;
        private readonly IBookRepository bookRepository;
        private readonly IBookAuthorAssociationRepository repository;
        private readonly IAuthorRepository authorRepository;

        public GetBooksByAuthorIdQueryHandler(IBookAuthorAssociationRepository repository, IBookRepository bookRepository, IBookGenreAssociationRepository genreAssociationRepository, IAuthorRepository authorRepository, IGenreRepository genreRepository)
        {
            this.repository = repository;
            this.bookRepository = bookRepository;
            this.genreAssociationRepository = genreAssociationRepository;
            this.authorRepository = authorRepository;
            this.genreRepository = genreRepository;
        }

        public async Task<IEnumerable<BookDtoFE>> Handle(GetBooksByAuthorIdQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Guid> bookIds = await repository.GetBooksByAuthorId(request.AuthorId);
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
                ICollection<Guid> genreIds = await genreAssociationRepository.GetGenresByBookId(fetchedBook.Id);
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
                ICollection<Guid> authorIds = await repository.GetAuthorsByBookId(fetchedBook.Id);
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
