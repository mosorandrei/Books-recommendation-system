using Application.Interfaces;
using MediatR;

namespace Application.Features.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly IUserRepository repository;

        public UpdateUserCommandHandler(IUserRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = repository.GetByIdAsync(request.Id).Result;
            if (user == null || user.Id == Guid.Empty)
            {
                throw new InvalidDataException("User does not exist!");
            }
            user.Username = request.Username;
            user.Password = request.Password;

            await repository.UpdateAsync(user);

            return user.Id;
        }
    }
}
