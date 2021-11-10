using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetGenreByIdQueryHandler : IRequestHandler<GetGenreByIdQuery, Genre>
    {
        private readonly IGenreRepository repository;

        public GetGenreByIdQueryHandler(IGenreRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Genre> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await repository.GetByIdAsync(request.Id);
            if (book == null)
            {
                throw new Exception("Book does not exist!");
            }
            return book;
        }
    }
}
