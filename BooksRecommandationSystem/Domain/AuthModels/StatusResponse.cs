namespace Domain.AuthModels
{
    public class StatusResponse
    {
        public string? UserId { get; set; }
        public Guid? BookId { get; set; }
        public string? Status { get; set; }
    }
}
