using MediatR;

namespace Application.Features.Commands
{
    public class CreateBookCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public decimal Rating { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public Uri ImageUri { get; set; }
    }
}
