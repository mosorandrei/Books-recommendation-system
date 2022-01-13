using Application.MachineLearning.DataModels;
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
        protected static string ModelPath => Path.Combine(AppContext.BaseDirectory, "recommender.mdl");
        protected readonly MLContext MlContext;
        protected DataOperationsCatalog.TrainTestData _dataSplit;
        protected ITrainerEstimator<MatrixFactorizationPredictionTransformer, MatrixFactorizationModelParameters>? _model;
        protected ITransformer? _trainedModel;

        protected TrainerBase()
        {
            MlContext = new MLContext(111);
        }

        /// <summary>
        /// Train model on defined data.
        /// </summary>
        /// <param name="trainingFileName"></param>
        public void Fit(List<ReadingStatus> ReadingStatuses)
        {
            if (ReadingStatuses.Count == 0)
            {
                throw new ArgumentNullException($"List of Statuses provided for ML.NET is empty!");
            }

            _dataSplit = LoadAndPrepareData(ReadingStatuses);
            var dataProcessPipeline = BuildDataProcessingPipeline();
            var trainingPipeline = dataProcessPipeline.Append(_model);

            _trainedModel = trainingPipeline.Fit(_dataSplit.TrainSet);
        }

        /// <summary>
        /// Evaluate trained model.
        /// </summary>
        /// <returns>RegressionMetrics object.</returns>
        public RegressionMetrics Evaluate()
        {
            if (_trainedModel == null)
                throw new ArgumentNullException("Error when recommending Books!");
            var testSetTransform = _trainedModel.Transform(_dataSplit.TestSet);

            return MlContext.Regression.Evaluate(testSetTransform);
        }

        /// <summary>
        /// Save Model in the file.
        /// </summary>
        public void Save()
        {
            MlContext.Model.Save(_trainedModel, _dataSplit.TrainSet.Schema, ModelPath);
        }

        /// <summary>
        /// Feature engeneering and data pre-processing.
        /// </summary>
        /// <returns>Data Processing Pipeline.</returns>
        private EstimatorChain<ValueToKeyMappingTransformer> BuildDataProcessingPipeline()
        {
            var dataProcessPipeline = MlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "UserId", outputColumnName: "UserIdEncoded")
                .Append(MlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "BookId", outputColumnName: "BookIdEncoded"))
                .AppendCacheCheckpoint(MlContext);

            return dataProcessPipeline;
        }

        private DataOperationsCatalog.TrainTestData LoadAndPrepareData(List<ReadingStatus> ReadingStatuses)
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
    }
}
