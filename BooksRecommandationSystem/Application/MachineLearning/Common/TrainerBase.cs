using Application.MachineLearning.DataModels;
using Domain.AuthModels;
using Domain.Entities;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Trainers.Recommender;
using Microsoft.ML.Transforms;

namespace Application.MachineLearning.Common
{
    /// <summary>
    /// Base class for Trainers.
    /// This class exposes methods for training, evaluating and saving ML Models.
    /// </summary>
    public abstract class TrainerBase : ITrainerBase
    {
        public string? Name { get; protected set; }
        protected static string ModelPathSimilarity => Path.Combine(AppContext.BaseDirectory, "recommenderSimilarity.mdl");
        protected static string ModelPathRating => Path.Combine(AppContext.BaseDirectory, "recommenderRating.mdl");
        protected readonly MLContext MlContext;
        protected DataOperationsCatalog.TrainTestData _dataSplit;
        protected ITrainerEstimator<MatrixFactorizationPredictionTransformer, MatrixFactorizationModelParameters>? _modelRating;
        protected ITrainerEstimator<MatrixFactorizationPredictionTransformer, MatrixFactorizationModelParameters>? _modelSimilarity;
        protected ITransformer? _trainedModelRating;
        protected ITransformer? _trainedModelSimilarity;

        protected TrainerBase()
        {
            MlContext = new MLContext(111);
        }

        /// <summary>
        /// Train model on defined data.
        /// </summary>
        public void FitScore(List<ReadingStatus> ReadingStatuses)
        {
            if (ReadingStatuses.Count == 0)
            {
                throw new ArgumentNullException($"List of Statuses provided for ML.NET is empty!");
            }

            _dataSplit = LoadAndPrepareDataStatuses(ReadingStatuses);
            var dataProcessPipeline = BuildDataProcessingPipelineScore();
            var trainingPipeline = dataProcessPipeline.Append(_modelRating);

            _trainedModelRating = trainingPipeline.Fit(_dataSplit.TrainSet);
        }

        public void FitSimilarity(List<BookDtoFE> BookGuids)
        {
            if (BookGuids.Count == 0)
            {
                throw new ArgumentNullException($"List of Guids related by Genre and Author provided for ML.NET is empty!");
            }
            _dataSplit = LoadAndPrepareDataSimilarity(BookGuids);
            var dataProcessPipeline = BuildDataProcessingPipelineSimilarity();
            var trainingPipeline = dataProcessPipeline.Append(_modelSimilarity);

            _trainedModelSimilarity = trainingPipeline.Fit(_dataSplit.TrainSet);
        }

        /// <summary>
        /// Evaluate trained model.
        /// </summary>
        /// <returns>RegressionMetrics object.</returns>
        public RegressionMetrics EvaluateRating()
        {
            if (_trainedModelRating == null)
                throw new ArgumentNullException("Error when recommending Books by Rating!");
            var testSetTransform = _trainedModelRating.Transform(_dataSplit.TestSet);

            return MlContext.Regression.Evaluate(testSetTransform);
        }

        public RegressionMetrics EvaluateSimilarity()
        {
            if (_trainedModelSimilarity == null)
                throw new ArgumentNullException("Error when recommending Books by Similarity!");
            var testSetTransform = _trainedModelSimilarity.Transform(_dataSplit.TestSet);

            return MlContext.Regression.Evaluate(testSetTransform);
        }

        /// <summary>
        /// Save Model in the file.
        /// </summary>
        public void Save()
        {
            MlContext.Model.Save(_trainedModelSimilarity, _dataSplit.TrainSet.Schema, ModelPathSimilarity);
            MlContext.Model.Save(_trainedModelRating, _dataSplit.TrainSet.Schema, ModelPathRating);
        }

        /// <summary>
        /// Feature engeneering and data pre-processing.
        /// </summary>
        /// <returns>Data Processing Pipeline.</returns>
        private EstimatorChain<ValueToKeyMappingTransformer> BuildDataProcessingPipelineScore()
        {
            var dataProcessPipeline = MlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "UserId", outputColumnName: "UserIdEncoded")
                .Append(MlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "BookId", outputColumnName: "BookIdEncoded"))
                .AppendCacheCheckpoint(MlContext);

            return dataProcessPipeline;
        }

        private EstimatorChain<ValueToKeyMappingTransformer> BuildDataProcessingPipelineSimilarity()
        {
            var dataProcessPipeline = MlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "BookId", outputColumnName: "BookIdEncoded")
                .Append(MlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "SimilarBookId", outputColumnName: "SimilarBookIdEncoded"))
                .AppendCacheCheckpoint(MlContext);

            return dataProcessPipeline;
        }

        private DataOperationsCatalog.TrainTestData LoadAndPrepareDataStatuses(List<ReadingStatus> ReadingStatuses)
        {
            List<BookRating> bookRatings = new();
            foreach (ReadingStatus readingStatus in ReadingStatuses)
            {
                if (readingStatus.Status == Domain.Constants.ReadingStatusEnum.Read && readingStatus.UserScore != 0)
                {
                    bookRatings.Add(new BookRating()
                    {
                        UserId = readingStatus.ApplicationUserId,
                        BookId = readingStatus.BookId.ToString(),
                        Label = readingStatus.UserScore
                    });
                }
            }
            IDataView trainingDataView = MlContext.Data.LoadFromEnumerable(bookRatings.AsEnumerable());
            return MlContext.Data.TrainTestSplit(trainingDataView, testFraction: 0.1);
        }

        private DataOperationsCatalog.TrainTestData LoadAndPrepareDataSimilarity(List<BookDtoFE> BookDtos)
        {
            List<BookSimilarity> bookSimilarities = new();
            for (int i = 0; i < BookDtos.Count - 1; i++)
            {
                for (int j = i + 1; j < BookDtos.Count; j++)
                {
                    float currentScore = 0;
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8604 // Possible null reference argument.
                    if (BookDtos[i].Genres.Intersect(BookDtos[j].Genres).Any())
                        currentScore++;
                    if (BookDtos[i].Authors.Intersect(BookDtos[j].Authors).Any())
                        currentScore++;
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8604 // Possible null reference argument.
                    bookSimilarities.Add(new BookSimilarity()
                    {
                        BookId = BookDtos[i].ToString(),
                        SimilarBookId = BookDtos[j].ToString(),
                        Label = currentScore
                    });
                }
            }

            IDataView trainingDataView = MlContext.Data.LoadFromEnumerable(bookSimilarities.AsEnumerable());
            return MlContext.Data.TrainTestSplit(trainingDataView, testFraction: 0.1);
        }
    }
}
