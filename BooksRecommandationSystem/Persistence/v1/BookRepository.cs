using Application.Interfaces;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.v1
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(BookContext context) : base(context)
        {
        }
    }
}
