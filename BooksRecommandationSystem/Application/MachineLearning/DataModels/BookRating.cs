namespace Application.MachineLearning.DataModels
{
    public class BookRating
    {
        public string? UserId { get; set; }
        public string? BookId { get; set; }
        public float Label { get; set; }
    }
}
