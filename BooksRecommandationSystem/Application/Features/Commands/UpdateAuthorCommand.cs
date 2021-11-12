using MediatR;

namespace Application.Features.Commands
{
    public class UpdateAuthorCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
