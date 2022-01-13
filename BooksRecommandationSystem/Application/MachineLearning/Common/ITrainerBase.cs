using Domain.Entities;
using Microsoft.ML.Data;

namespace Application.MachineLearning.Common
{
    public interface ITrainerBase
    {
        string? Name { get; }
        void Fit(List<ReadingStatus> ReadingStatuses);
        RegressionMetrics Evaluate();
        void Save();
    }
}
