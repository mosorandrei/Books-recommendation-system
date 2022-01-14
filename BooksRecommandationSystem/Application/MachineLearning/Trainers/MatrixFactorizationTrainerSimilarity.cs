using Application.MachineLearning.Common;
using Microsoft.ML;

namespace Application.MachineLearning.Trainers
{
    public class MatrixFactorizationTrainerSimilarity : TrainerBase
    {
        public MatrixFactorizationTrainerSimilarity(int numberOfIterations, int approximationRank, double learningRate) : base()
        {
            Name = $"Matrix Factorization {numberOfIterations}-{approximationRank}";

            _modelSimilarity = MlContext.Recommendation().Trainers.MatrixFactorization(
                                                      labelColumnName: "Label",
                                                      matrixColumnIndexColumnName: "BookIdEncoded",
                                                      matrixRowIndexColumnName: "SimilarBookIdEncoded",
                                                      approximationRank: approximationRank,
                                                      learningRate: learningRate,
                                                      numberOfIterations: numberOfIterations);
        }
    }
}
