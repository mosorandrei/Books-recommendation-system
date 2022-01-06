namespace Domain.AuthModels
{
    public class FavouritesResponse
    {
        public string? UserId { get; set; }
        public Guid? BookId { get; set; }
        public string? Status { get; set; }
    }
}

