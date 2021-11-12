using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Guid>
    {
        private readonly IAuthorRepository repository;

        public DeleteAuthorCommandHandler(IAuthorRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Guid> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = repository.GetByIdAsync(request.Id).Result;
            if (author == null)
            {
                throw new InvalidDataException("Author does not exist!");
            }
            await repository.DeleteAsync(author);
            return author.Id;
        }
    }
}
