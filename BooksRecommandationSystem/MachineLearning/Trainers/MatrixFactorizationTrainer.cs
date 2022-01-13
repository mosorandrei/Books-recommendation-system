using Application.MachineLearning.Common;
using Microsoft.ML;

namespace MachineLearning.Trainers
{
    public sealed class MatrixFactorizationTrainer : TrainerBase
    {
        public MatrixFactorizationTrainer(int numberOfIterations, int approximationRank, double learningRate) : base()
        {
            Name = $"Matrix Factorization {numberOfIterations}-{approximationRank}";

            _model = MlContext.Recommendation().Trainers.MatrixFactorization(
                                                      labelColumnName: "Label",
                                                      matrixColumnIndexColumnName: "UserIdEncoded",
                                                      matrixRowIndexColumnName: "MovieIdEncoded",
                                                      approximationRank: approximationRank,
                                                      learningRate: learningRate,
                                                      numberOfIterations: numberOfIterations);
        }
    }
}
