using Application.Interfaces;
using MediatR;

namespace Application.Features.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Guid>
    {
        private readonly IUserRepository repository;

        public DeleteUserCommandHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Guid> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = repository.GetByIdAsync(request.Id).Result;
            if (user == null)
            {
                throw new Exception("User does not exist!");
            }
            await repository.DeleteAsync(user);
            return user.Id;
        }
    }
}
