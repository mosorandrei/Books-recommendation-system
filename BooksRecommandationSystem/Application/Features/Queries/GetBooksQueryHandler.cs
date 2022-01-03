using Application.Interfaces;
using Domain.AuthModels;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<BookDtoFE>>
    {
        private readonly IBookRepository repository;
        private readonly IBookAuthorAssociationRepository authorAssociationRepository;
        private readonly IAuthorRepository authorRepository;
        private readonly IBookGenreAssociationRepository genreAssociationRepository;
        private readonly IGenreRepository genreRepository;

        public GetBooksQueryHandler(IBookRepository repository, IBookAuthorAssociationRepository authorAssociationRepository, IBookGenreAssociationRepository genreAssociationRepository, IAuthorRepository authorRepository, IGenreRepository genreRepository)
        {
            this.repository = repository;
            this.authorAssociationRepository = authorAssociationRepository;
            this.genreAssociationRepository = genreAssociationRepository;
            this.authorRepository = authorRepository;
            this.genreRepository = genreRepository;
        }
        public async Task<IEnumerable<BookDtoFE>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Book> books = await repository.GetAllAsync();
            List<BookDtoFE> bookDtos = new();
            foreach (Book book in books)
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