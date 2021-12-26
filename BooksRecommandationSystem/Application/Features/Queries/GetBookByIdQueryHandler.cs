using Application.Interfaces;
using Domain.AuthModels;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDtoFE>
    {
        private readonly IBookRepository repository;
        private readonly IBookAuthorAssociationRepository authorAssociationRepository;
        private readonly IAuthorRepository authorRepository;
        private readonly IBookGenreAssociationRepository genreAssociationRepository;
        private readonly IGenreRepository genreRepository;

        public GetBookByIdQueryHandler(IBookRepository repository, IBookAuthorAssociationRepository authorAssociationRepository, IBookGenreAssociationRepository genreAssociationRepository, IAuthorRepository authorRepository, IGenreRepository genreRepository)
        {
            this.repository = repository;
            this.authorAssociationRepository = authorAssociationRepository;
            this.genreAssociationRepository = genreAssociationRepository;
            this.authorRepository = authorRepository;
            this.genreRepository = genreRepository;
        }

        public async Task<BookDtoFE> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await repository.GetByIdAsync(request.Id);
            if (book == null)
            {
                throw new InvalidDataException("Book does not exist!");
            }
            var bookDto = new BookDtoFE
            {
                BookId = book.Id,
                Title = book.Title,
                Rating = book.Rating,
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

            return bookDto;
        }
    }
}
