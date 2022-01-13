using Application.MachineLearning.DataModels;
using Microsoft.ML;

namespace Application.MachineLearning.Predictors
{
    public class PredictorRating
    {
        protected static string ModelPath => Path.Combine(AppContext.BaseDirectory, "recommenderRating.mdl");
        private readonly MLContext _mlContext;

        private ITransformer? _modelRating;

        public PredictorRating()
        {
            _mlContext = new MLContext(111);
        }

        /// <summary>
        /// Runs prediction on new data.
        /// </summary>
        /// <param name="newSample">New data sample.</param>
        /// <returns>Prediction object</returns>
        public BookRatingPrediction Predict(BookRating newSample)
        {
            LoadModel();

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<BookRating, BookRatingPrediction>(_modelRating);

            return predictionEngine.Predict(newSample);
        }

        private void LoadModel()
        {
            if (!File.Exists(ModelPath))
            {
                throw new FileNotFoundException($"File {ModelPath} doesn't exist.");
            }

            using (var stream = new FileStream(ModelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                _modelRating = _mlContext.Model.Load(stream, out _);
            }

            if (_modelRating == null)
            {
                throw new Exception($"Failed to load Model");
            }
        }
    }
}
