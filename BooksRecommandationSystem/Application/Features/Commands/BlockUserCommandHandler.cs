using Application.Interfaces;
using MediatR;

namespace Application.Features.Commands
{
    public class BlockUserCommandHandler : IRequestHandler<BlockUserCommand, string>
    {
        private readonly ITokenRepository repository;

        public BlockUserCommandHandler(ITokenRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Handle(BlockUserCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
                throw new ArgumentNullException(nameof(request.UserId));
            return await repository.BlockUser(request.UserId);
        }
    }
}
