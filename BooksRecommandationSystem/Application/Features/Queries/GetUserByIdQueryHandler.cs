using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUserRepository repository;

        public GetUserByIdQueryHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await repository.GetByIdAsync(request.Id);
            if (user == null)
            {
                throw new Exception("User does not exist!");
            }
            return user;
        }
    }
}
