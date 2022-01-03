using Application.Features.Responses;
using MediatR;

namespace Application.Features.Commands
{
    public class RateBookCommand : IRequest<BookStatusChangeResponse>
    {
        public string? UserId { get; set; }
        public Guid BookId { get; set; }
        public int Score { get; set; }
    }
}
