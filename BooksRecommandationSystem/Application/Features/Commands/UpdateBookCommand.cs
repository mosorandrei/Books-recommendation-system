using MediatR;

namespace Application.Features.Commands
{
    public class UpdateBookCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public decimal Rating { get; set; }
        public string? Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public Uri? ImageUri { get; set; }
        public Uri? DownloadUri { get; set; }
    }
}
