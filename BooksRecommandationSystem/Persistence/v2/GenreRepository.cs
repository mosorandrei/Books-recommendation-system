using Application.Interfaces;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.v2
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
