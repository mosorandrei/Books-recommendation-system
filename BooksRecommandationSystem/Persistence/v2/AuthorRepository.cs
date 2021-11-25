using Application.Interfaces;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.v2
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(AuthorContext context) : base(context)
        {
        }
    }
}
