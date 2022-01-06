using Application.Interfaces;
using Domain.AuthModels;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetUserReadingStatusesQueryHandler : IRequestHandler<GetUserReadingStatusesQuery, IEnumerable<UserReadingStatusesDtoFE>>
    {
        private readonly IReadingStatusRepository repository;
        private readonly IBookRepository bookRepository;
        private readonly IBookAuthorAssociationRepository authorAssociationRepository;
        private readonly IAuthorRepository authorRepository;
        private readonly IBookGenreAssociationRepository genreAssociationRepository;
        private readonly IGenreRepository genreRepository;

        public GetUserReadingStatusesQueryHandler(IReadingStatusRepository repository, IBookRepository bookRepository, IBookAuthorAssociationRepository authorAssociationRepository, IAuthorRepository authorRepository, IBookGenreAssociationRepository genreAssociationRepository, IGenreRepository genreRepository)
        {
            this.repository = repository;
            this.bookRepository = bookRepository;
            this.authorAssociationRepository = authorAssociationRepository;
            this.authorRepository = authorRepository;
            this.genreAssociationRepository = genreAssociationRepository;
            this.genreRepository = genreRepository;
        }

        public async Task<IEnumerable<UserReadingStatusesDtoFE>> Handle(GetUserReadingStatusesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<ReadingStatus> allStatuses = await repository.GetAllAsync();
            List<UserReadingStatusesDtoFE> userStatusesDtos = new();
            foreach (ReadingStatus status in allStatuses)
            {
                if (status.ApplicationUserId == request.UserId)
                {
                    var book = await bookRepository.GetByIdAsync(status.BookId);
                    if (book == null)
                    {
                        throw new InvalidDataException("Book does not exist!");
                    }
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
                    userStatusesDtos.Add(
                        new UserReadingStatusesDtoFE
                        {
                            Book = bookDto,
                            Status = status.Status,
                            IsFavourited = status.IsFavourited,
                            UserScore = status.UserScore
                        }
                        );
                }
            }
            return userStatusesDtos;
        }
    }
}
