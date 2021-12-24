using Application.Interfaces;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.v2
{
    public class ReadingStatusRepository : Repository<ReadingStatus>, IReadingStatusRepository
    {
        public ReadingStatusRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
