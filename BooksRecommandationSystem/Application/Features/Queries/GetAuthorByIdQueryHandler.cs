using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author>
    {
        private readonly IAuthorRepository repository;

        public GetAuthorByIdQueryHandler(IAuthorRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Author> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await repository.GetByIdAsync(request.Id);
            if (author == null)
            {
                throw new InvalidDataException("Author does not exist!");
            }
            return author;
        }
    }
}
