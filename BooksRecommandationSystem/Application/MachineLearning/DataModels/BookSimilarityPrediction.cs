namespace Application.MachineLearning.DataModels
{
    public class BookSimilarityPrediction
    {
        public string? BookId { get; set; }
        public string? SimilarBookId { get; set; }
        public float Score { get; set; }
    }
}
