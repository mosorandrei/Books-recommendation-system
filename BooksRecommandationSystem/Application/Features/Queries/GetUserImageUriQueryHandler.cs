using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetUserImageUriQueryHandler : IRequestHandler<GetUserImageUriQuery, Uri>
    {
        private readonly ITokenRepository repository;

        public GetUserImageUriQueryHandler(ITokenRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Uri> Handle(GetUserImageUriQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
                throw new ArgumentNullException(nameof(request.UserId));
            ApplicationUser user = await repository.GetUserById(request.UserId);
#pragma warning disable CS8603 // Possible null reference return.
            return user.ImageUri;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
