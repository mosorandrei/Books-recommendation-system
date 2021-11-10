using Application.Interfaces;
using MediatR;

namespace Application.Features.Commands
{
    public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, Guid>
    {
        private readonly IGenreRepository repository;

        public UpdateGenreCommandHandler(IGenreRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Guid> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = repository.GetByIdAsync(request.Id).Result;
            if (genre == null || genre.Id == Guid.Empty)
            {
                throw new Exception("Genre does not exist!");
            }
            genre.Name = request.Name;

            await repository.UpdateAsync(genre);

            return genre.Id;
        }
    }
}
