using Application.Interfaces;
using MediatR;

namespace Application.Features.Commands
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Guid>
    {
        private readonly IAuthorRepository repository;

        public UpdateAuthorCommandHandler(IAuthorRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Guid> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = repository.GetByIdAsync(request.Id).Result;
            if (author == null || author.Id == Guid.Empty)
            {
                throw new InvalidDataException("Author does not exist!");
            }
            author.FirstName = request.FirstName;
            author.LastName = request.LastName;

            await repository.UpdateAsync(author);

            return author.Id;
        }
    }
}
