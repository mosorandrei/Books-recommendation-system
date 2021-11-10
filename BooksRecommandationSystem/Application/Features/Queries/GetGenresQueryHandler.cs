using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetGenresQueryHandler : IRequestHandler<GetGenresQuery, IEnumerable<Genre>>
    {
        private readonly IGenreRepository repository;
        public GetGenresQueryHandler(IGenreRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IEnumerable<Genre>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAllAsync();
        }
    }
}
