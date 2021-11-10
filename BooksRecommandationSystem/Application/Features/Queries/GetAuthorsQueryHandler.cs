using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<Author>>
    {
        private readonly IAuthorRepository repository;

        public GetAuthorsQueryHandler(IAuthorRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IEnumerable<Author>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAllAsync();
        }
    }
}
