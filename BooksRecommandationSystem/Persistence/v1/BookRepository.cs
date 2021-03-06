using Application.Interfaces;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.v1
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(DatabaseContext context) : base(context)
        {
        }
        public Task<string> UpdateBookReviews(Guid BookId, int PreviousScore, int Score)
        {
            throw new NotImplementedException();
        }
    }
}
