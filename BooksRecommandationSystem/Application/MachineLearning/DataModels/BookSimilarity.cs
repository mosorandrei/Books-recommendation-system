namespace Application.MachineLearning.DataModels
{
    public class BookSimilarity
    {
        public string? BookId { get; set; }
        public string? SimilarBookId { get; set; }
        public float Label { get; set; }
    }
}
