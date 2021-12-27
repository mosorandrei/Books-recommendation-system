using MediatR;

namespace Application.Features.Commands
{
    public class AddAuthorToBookCommand : IRequest<string>
    {
        public Guid BookId { get; set; }
        public Guid AuthorId { get; set; }

    }
}
