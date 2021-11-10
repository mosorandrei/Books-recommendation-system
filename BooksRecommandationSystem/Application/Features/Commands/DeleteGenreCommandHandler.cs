using Application.Interfaces;
using MediatR;

namespace Application.Features.Commands
{
    public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, Guid>
    {
        private readonly IGenreRepository repository;

        public DeleteGenreCommandHandler(IGenreRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Guid> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = repository.GetByIdAsync(request.Id).Result;
            if (genre == null)
            {
                throw new Exception("Genre does not exist!");
            }
            await repository.DeleteAsync(genre);
            return genre.Id;
        }
    }
}
