using Domain.AuthModels;
using Domain.Entities;
using Microsoft.ML.Data;

namespace Application.MachineLearning.Common
{
    public interface ITrainerBase
    {
        string? Name { get; }
        void FitScore(List<ReadingStatus> ReadingStatuses);
        void FitSimilarity(List<BookDtoFE> BookGuids);
        RegressionMetrics EvaluateRating();
        RegressionMetrics EvaluateSimilarity();
        void Save();
    }
}
