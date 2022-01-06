using Application.Interfaces;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.v2
{
    public class BookGenreAssociationRepository : Repository<BookGenreAssociation>, IBookGenreAssociationRepository
    {
        public BookGenreAssociationRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<ICollection<Guid>> GetBooksByGenreId(Guid GenreId)
        {
            ICollection<Guid> BookIds = new HashSet<Guid>();
            IEnumerable<BookGenreAssociation> BookGenreAssociations = await GetAllAsync();
            foreach (BookGenreAssociation association in BookGenreAssociations)
            {
                if (association.GenreId == GenreId)
                {
                    BookIds.Add(association.BookId);
                }
            }
            return BookIds;
        }

        public async Task<ICollection<Guid>> GetGenresByBookId(Guid BookId)
        {
            ICollection<Guid> GenreIds = new HashSet<Guid>();
            IEnumerable<BookGenreAssociation> BookGenreAssociations = await GetAllAsync();
            foreach (BookGenreAssociation association in BookGenreAssociations)
            {
                if (association.BookId == BookId)
                {
                    GenreIds.Add(association.GenreId);
                }
            }
            return GenreIds;
        }
    }
}
