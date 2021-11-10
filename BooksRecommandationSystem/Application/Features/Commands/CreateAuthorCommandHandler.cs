using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Guid>
    {
        private readonly IAuthorRepository repository;

        public CreateAuthorCommandHandler(IAuthorRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Guid> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Author
            {
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            await repository.AddAsync(author);
            return author.Id;
        }
    }
}
