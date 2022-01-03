using Application.Interfaces;
using Domain.AuthModels;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetMyReadingsQueryHandler : IRequestHandler<GetMyReadingsQuery, IEnumerable<BookDtoFE>>
    {
        private readonly IReadingStatusRepository repository;
        private readonly IBookRepository bookRepository;
        private readonly IBookAuthorAssociationRepository authorAssociationRepository;
        private readonly IAuthorRepository authorRepository;
        private readonly IBookGenreAssociationRepository genreAssociationRepository;
        private readonly IGenreRepository genreRepository;

        public GetMyReadingsQueryHandler(IReadingStatusRepository repository, IBookRepository bookRepository, IBookAuthorAssociationRepository authorAssociationRepository, IAuthorRepository authorRepository, IBookGenreAssociationRepository genreAssociationRepository, IGenreRepository genreRepository)
        {
            this.repository = repository;
            this.bookRepository = bookRepository;
            this.authorAssociationRepository = authorAssociationRepository;
            this.authorRepository = authorRepository;
            this.genreAssociationRepository = genreAssociationRepository;
            this.genreRepository = genreRepository;
        }
        public async Task<IEnumerable<BookDtoFE>> Handle(GetMyReadingsQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
                throw new ArgumentNullException(request.UserId);
            ICollection<Guid> BookIds = await repository.GetMyReadings(request.UserId);
            List<Book> FetchedBooks = new();
            foreach (Guid guid in BookIds)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                Book CurrentBook = await bookRepository.GetByIdAsync(guid);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                if (CurrentBook == null)
                    throw new NullReferenceException(nameof(CurrentBook));
                FetchedBooks.Add(CurrentBook);
            }
            List<BookDtoFE> bookDtos = new();
            foreach (Book book in FetchedBooks)
            {
                var bookDto = new BookDtoFE
                {
                    BookId = book.Id,
                    Title = book.Title,
                    Rating = book.Rating,
                    NumberOfReviews = book.NumberOfReviews,
                    Description = book.Description,
                    DownloadUri = book.DownloadUri,
                    PublicationDate = book.PublicationDate,
                    UploadDate = book.UploadDate,
                    ImageUri = book.ImageUri
                };

                ICollection<Guid> genreIds = await genreAssociationRepository.GetGenresByBookId(book.Id);
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

                ICollection<Guid> authorIds = await authorAssociationRepository.GetAuthorsByBookId(book.Id);
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
