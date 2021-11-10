using MediatR;

namespace Application.Features.Commands
{
    public class DeleteAuthorCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
