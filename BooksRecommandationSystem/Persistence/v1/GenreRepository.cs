using Application.Interfaces;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.v1
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(GenreContext context) : base(context)
        {
        }
    }
}
