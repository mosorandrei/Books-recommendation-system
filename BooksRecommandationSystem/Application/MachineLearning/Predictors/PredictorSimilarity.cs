using Application.MachineLearning.DataModels;
using Microsoft.ML;

namespace Application.MachineLearning.Predictors
{
    public class PredictorSimilarity
    {
        protected static string ModelPath => Path.Combine(AppContext.BaseDirectory, "recommenderSimilarity.mdl");
        private readonly MLContext _mlContext;

        private ITransformer? _modelSimilarity;

        public PredictorSimilarity()
        {
            _mlContext = new MLContext(111);
        }

        /// <summary>
        /// Runs prediction on new data.
        /// </summary>
        /// <param name="newSample">New data sample.</param>
        /// <returns>Prediction object</returns>
        public BookSimilarityPrediction Predict(BookSimilarity newSample)
        {
            LoadModel();

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<BookSimilarity, BookSimilarityPrediction>(_modelSimilarity);

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
                _modelSimilarity = _mlContext.Model.Load(stream, out _);
            }

            if (_modelSimilarity == null)
            {
                throw new Exception($"Failed to load Model");
            }
        }
    }
}
