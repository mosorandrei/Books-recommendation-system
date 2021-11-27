using Domain.Common;

namespace Domain.Entities
{
    public class Book : BaseEntity
    {
        public string? Title { get; set; }
        public decimal Rating { get; set; }
        public string? Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public Uri? ImageUri { get; set; }
        public Uri? DownloadUri { get; set; }   
    }
}
