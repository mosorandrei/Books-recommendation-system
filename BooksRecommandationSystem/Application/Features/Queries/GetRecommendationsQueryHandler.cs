using Application.Interfaces;
using Application.MachineLearning.Common;
using Application.MachineLearning.DataModels;
using Application.MachineLearning.Predictors;
using Application.MachineLearning.Trainers;
using Domain.AuthModels;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetRecommendationsQueryHandler : IRequestHandler<GetRecommendationsQuery, IEnumerable<BookDtoFE>>
    {
        private readonly IBookRepository repository;
        private readonly IBookAuthorAssociationRepository authorAssociationRepository;
        private readonly IAuthorRepository authorRepository;
        private readonly IBookGenreAssociationRepository genreAssociationRepository;
        private readonly IGenreRepository genreRepository;
        private readonly IReadingStatusRepository readingStatusRepository;

        public GetRecommendationsQueryHandler(IBookRepository repository, IBookAuthorAssociationRepository authorAssociationRepository, IBookGenreAssociationRepository genreAssociationRepository, IAuthorRepository authorRepository, IGenreRepository genreRepository, IReadingStatusRepository readingStatusRepository)
        {
            this.repository = repository;
            this.authorAssociationRepository = authorAssociationRepository;
            this.genreAssociationRepository = genreAssociationRepository;
            this.authorRepository = authorRepository;
            this.genreRepository = genreRepository;
            this.readingStatusRepository = readingStatusRepository;
        }
        public async Task<IEnumerable<BookDtoFE>> Handle(GetRecommendationsQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
                throw new ArgumentNullException("Request UserId is null!");
            IEnumerable<ReadingStatus> readingStatuses = await readingStatusRepository.GetAllAsync();
            List<ReadingStatus> readingStatusesToBeRecommended = new();
            foreach (ReadingStatus readingStatus in readingStatuses)
            {
                if (readingStatus.ApplicationUserId == request.UserId && readingStatus.IsFavourited == false && readingStatus.Status == Domain.Constants.ReadingStatusEnum.ToBeReaded)
                    readingStatusesToBeRecommended.Add(readingStatus);
            }

            if (readingStatuses.ToList().Count == 0)
                throw new ArgumentException("Insufficient data in order to get Recommendations!");
            if (readingStatusesToBeRecommended.Count == 0)
                throw new ArgumentException("No recommendations available! You read all the Books!");

            List<BookRatingPrediction> Predictions = GetPredictions(readingStatuses.ToList(), readingStatusesToBeRecommended);
            Predictions.Sort((p, q) => p.Score.CompareTo(q.Score));
            Predictions.Reverse();

            List<Book> books = new();

            var count = 0;

            Console.WriteLine("*******************************");
            Console.WriteLine("Printing our Recommended Books' predicted scores! - at most 5 Recommendations");
            Console.WriteLine("*******************************");

            foreach (BookRatingPrediction prediction in Predictions)
            {
                Console.WriteLine(prediction.BookId + " - with predicted Score = " + prediction.Score);
                if (count == 5)
                    break;
                if (prediction.BookId == null)
                    throw new ArgumentNullException("Error when predicting : BookId is null!");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                Book toBeAdded = await repository.GetByIdAsync(Guid.Parse(prediction.BookId));
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                if (toBeAdded == null)
                    throw new ArgumentNullException("Error when fetching requested Book, not found!!");
                books.Add(toBeAdded);
                count++;
            }
            Console.WriteLine("*******************************");

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

        private static List<BookRatingPrediction> GetPredictions(List<ReadingStatus> trainData, List<ReadingStatus> toBeRecommended)
        {
            List<BookRating> movieRatings = new();
            foreach (ReadingStatus status in toBeRecommended)
            {
                movieRatings.Add(new BookRating()
                {
                    UserId = status.ApplicationUserId,
                    BookId = status.BookId.ToString()
                });
            }

            var trainer = new MatrixFactorizationTrainer(10, 50, 0.1);

            List<BookRatingPrediction> bookRatingPredictions = new();

            foreach (BookRating sample in movieRatings)
                bookRatingPredictions.Add(TrainEvaluatePredict(trainData, trainer, sample));

            return bookRatingPredictions;
        }

        private static BookRatingPrediction TrainEvaluatePredict(List<ReadingStatus> trainData, ITrainerBase trainer, BookRating newSample)
        {
            Console.WriteLine("*******************************");
            Console.WriteLine($"{ trainer.Name }");
            Console.WriteLine("*******************************");

            trainer.Fit(trainData);

            var modelMetrics = trainer.Evaluate();

            Console.WriteLine($"Loss Function: {modelMetrics.LossFunction:0.##}{Environment.NewLine}" +
                              $"Mean Absolute Error: {modelMetrics.MeanAbsoluteError:#.##}{Environment.NewLine}" +
                              $"Mean Squared Error: {modelMetrics.MeanSquaredError:#.##}{Environment.NewLine}" +
                              $"RSquared: {modelMetrics.RSquared:0.##}{Environment.NewLine}" +
                              $"Root Mean Squared Error: {modelMetrics.RootMeanSquaredError:#.##}");

            trainer.Save();

            var predictor = new Predictor();
            var prediction = predictor.Predict(newSample);
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Prediction: {prediction.BookId}");
            Console.WriteLine($"Prediction: {prediction.Score:#.##}");
            Console.WriteLine("------------------------------");
            return prediction;
        }
    }
}
