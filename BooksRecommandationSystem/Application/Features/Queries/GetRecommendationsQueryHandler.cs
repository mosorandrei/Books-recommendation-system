using Application.Interfaces;
using Application.MachineLearning.Common;
using Application.MachineLearning.DataModels;
using Application.MachineLearning.Predictors;
using Application.MachineLearning.Trainers;
using Domain.AuthModels;
using Domain.Constants;
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
            var RecommendationScores = new Dictionary<Guid, float>();

            if (request.UserId == null)
                throw new ArgumentNullException("Request UserId is null!");

            IEnumerable<ReadingStatus> readingStatuses = await readingStatusRepository.GetAllAsync();

            List<ReadingStatus> readingStatusesToBeRecommended = new();
            List<ReadingStatus> favourites = new();
            foreach (ReadingStatus readingStatus in readingStatuses)
            {
                if (readingStatus.ApplicationUserId == request.UserId && readingStatus.IsFavourited == true)
                    favourites.Add(readingStatus);
                if (readingStatus.ApplicationUserId == request.UserId && readingStatus.IsFavourited == false && readingStatus.Status == Domain.Constants.ReadingStatusEnum.ToBeReaded)
                {
                    readingStatusesToBeRecommended.Add(readingStatus);
                    RecommendationScores[readingStatus.BookId] = 0;
                }
            }
            if (readingStatusesToBeRecommended.Count == 0)
                throw new ArgumentException("No recommendations available! You read all the Books!");

            // SCORE PREDICTION COMPONENT

            if (readingStatuses.ToList().Count == 0)
                throw new ArgumentException("Insufficient data in order to get Recommendations!");

            List<BookRatingPrediction> PredictionsScores = GetPredictionsScore(readingStatuses.ToList(), readingStatusesToBeRecommended);

            foreach (BookRatingPrediction prediction in PredictionsScores)
            {
                if (float.IsNaN(prediction.Score))
                    prediction.Score = 0;
#pragma warning disable CS8604 // Possible null reference argument.
                RecommendationScores[Guid.Parse(prediction.BookId)] += prediction.Score;
#pragma warning restore CS8604 // Possible null reference argument.
            }

            // END OF SCORE PREDICTION COMPONENT

            // GENRE AND AUTHOR PREDICTION COMPONENT
            if (favourites.Count > 0)
            {
                IEnumerable<BookDtoFE> allBooks = await GetAllBooksDtoFes();
                List<BookSimilarityPrediction> PredictionsSimilarity = GetPredictionsSimilarity(allBooks.ToList(), favourites, readingStatusesToBeRecommended);
                foreach (BookSimilarityPrediction prediction in PredictionsSimilarity)
                {
                    if (float.IsNaN(prediction.Score))
                        prediction.Score = 0;
#pragma warning disable CS8604 // Possible null reference argument.
                    RecommendationScores[Guid.Parse(prediction.BookId)] += prediction.Score;
#pragma warning restore CS8604 // Possible null reference argument.
                }
            }
            else
            {
                Console.WriteLine("NO PREDICTIONS MADE WITH GENRES - NO FAVOURITES!");
            }

            // END OF GENRE PREDICTION COMPONENT

            // DISPLAYING RESULTS


            IEnumerable<KeyValuePair<Guid, float>> FinalRecommendations = RecommendationScores.OrderByDescending(key => key.Value).Take(MachineLearningConstants.RECOMMENDATION_FETCHED);

            List<Book> books = new();

            var count = 0;

            Console.WriteLine("*******************************");
            Console.WriteLine($"Printing our Recommended Books' predicted scores! - at most {MachineLearningConstants.RECOMMENDATION_FETCHED} Recommendations");
            Console.WriteLine("!!! If a score is NaN, it means that not enough information is available in order to evaluate it correctly !!!");
            Console.WriteLine("*******************************");

            foreach (KeyValuePair<Guid, float> prediction in FinalRecommendations)
            {
                Console.WriteLine(prediction.Key + " - with predicted Score = " + prediction.Value);
                if (count == MachineLearningConstants.RECOMMENDATION_FETCHED)
                    break;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                Book toBeAdded = await repository.GetByIdAsync(prediction.Key);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                if (toBeAdded == null)
                    throw new ArgumentNullException("Error when fetching requested Book, not found!!");
                books.Add(toBeAdded);
                count++;
            }
            Console.WriteLine("*******************************");

            // END OF DISPLAYING RESULTS

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

        private static List<BookRatingPrediction> GetPredictionsScore(List<ReadingStatus> trainData, List<ReadingStatus> toBeRecommended)
        {
            List<BookRating> bookRatings = new();
            foreach (ReadingStatus status in toBeRecommended)
            {
                bookRatings.Add(new BookRating()
                {
                    UserId = status.ApplicationUserId,
                    BookId = status.BookId.ToString()
                });
            }

            var trainer = new MatrixFactorizationTrainerRating(100, 50, 0.055);

            TrainEvaluateScoreModel(trainData, trainer);

            List<BookRatingPrediction> bookRatingPredictions = new();

            foreach (BookRating sample in bookRatings)
                bookRatingPredictions.Add(PredictScore(sample));

            return bookRatingPredictions;
        }

        private static void TrainEvaluateScoreModel(List<ReadingStatus> trainData, ITrainerBase trainer)
        {
            Console.WriteLine("*******************************");
            Console.WriteLine($"{ trainer.Name }");
            Console.WriteLine("*******************************");

            trainer.FitScore(trainData);

            var modelMetrics = trainer.EvaluateRating();

            Console.WriteLine($"Loss Function: {modelMetrics.LossFunction:0.##}{Environment.NewLine}" +
                              $"Mean Absolute Error: {modelMetrics.MeanAbsoluteError:#.##}{Environment.NewLine}" +
                              $"Mean Squared Error: {modelMetrics.MeanSquaredError:#.##}{Environment.NewLine}" +
                              $"RSquared: {modelMetrics.RSquared:0.##}{Environment.NewLine}" +
                              $"Root Mean Squared Error: {modelMetrics.RootMeanSquaredError:#.##}");
            trainer.SaveRating();

        }

        private static BookRatingPrediction PredictScore(BookRating newSample)
        {
            var predictor = new PredictorRating();
            var prediction = predictor.Predict(newSample);
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Prediction: {prediction.BookId}");
            Console.WriteLine($"Prediction: {prediction.Score:#.##}");
            Console.WriteLine("------------------------------");
            return prediction;
        }

        private static List<BookSimilarityPrediction> GetPredictionsSimilarity(List<BookDtoFE> trainData, List<ReadingStatus> Favourites, List<ReadingStatus> toBeRecommended)
        {
            List<BookSimilarity> bookSimilarities = new();
            foreach (ReadingStatus status in toBeRecommended)
            {
                foreach (ReadingStatus favStatus in Favourites)
                {
                    bookSimilarities.Add(new BookSimilarity()
                    {
                        BookId = status.BookId.ToString(),
                        SimilarBookId = favStatus.BookId.ToString()
                    });
                }
            }

            var trainer = new MatrixFactorizationTrainerSimilarity(100, 50, 0.055);

            TrainEvaluateSimilarityModel(trainData, trainer);

            List<BookSimilarityPrediction> bookRatingPredictions = new();

            foreach (BookSimilarity sample in bookSimilarities)
                bookRatingPredictions.Add(PredictSimilarity(sample));

            return bookRatingPredictions;
        }

        private static void TrainEvaluateSimilarityModel(List<BookDtoFE> trainData, ITrainerBase trainer)
        {
            Console.WriteLine("*******************************");
            Console.WriteLine($"{ trainer.Name }");
            Console.WriteLine("*******************************");

            trainer.FitSimilarity(trainData);

            var modelMetrics = trainer.EvaluateSimilarity();

            Console.WriteLine($"Loss Function: {modelMetrics.LossFunction:0.##}{Environment.NewLine}" +
                              $"Mean Absolute Error: {modelMetrics.MeanAbsoluteError:#.##}{Environment.NewLine}" +
                              $"Mean Squared Error: {modelMetrics.MeanSquaredError:#.##}{Environment.NewLine}" +
                              $"RSquared: {modelMetrics.RSquared:0.##}{Environment.NewLine}" +
                              $"Root Mean Squared Error: {modelMetrics.RootMeanSquaredError:#.##}");

            trainer.SaveSimilarity();
        }

        private static BookSimilarityPrediction PredictSimilarity(BookSimilarity newSample)
        {
            var predictor = new PredictorSimilarity();
            var prediction = predictor.Predict(newSample);
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Prediction: {newSample.BookId} and {newSample.SimilarBookId}");
            Console.WriteLine($"Prediction: {prediction.Score:#.##}");
            Console.WriteLine("------------------------------");
            return prediction;
        }

        private async Task<IEnumerable<BookDtoFE>> GetAllBooksDtoFes()
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
