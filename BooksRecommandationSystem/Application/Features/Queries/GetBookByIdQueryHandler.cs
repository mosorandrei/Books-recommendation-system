using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IBookRepository repository;

        public GetBookByIdQueryHandler(IBookRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
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
