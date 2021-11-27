using Application.Interfaces;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.v2
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(BookContext context) : base(context)
        {
        }
    }
}
