using Application.Interfaces;
using MediatR;

namespace Application.Features.Commands
{
    public class UnblockUserCommandHandler : IRequestHandler<UnblockUserCommand, string>
    {
        private readonly ITokenRepository repository;

        public UnblockUserCommandHandler(ITokenRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(UnblockUserCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
                throw new ArgumentNullException(nameof(request.UserId));
            return await repository.UnblockUser(request.UserId);
        }
    }
}
