using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands
{
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, Guid>
    {
        private readonly IGenreRepository repository;

        public CreateGenreCommandHandler(IGenreRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Guid> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = new Genre
            {
                Name = request.Name
            };

            await repository.AddAsync(genre);
            return genre.Id;
        }
    }
}
