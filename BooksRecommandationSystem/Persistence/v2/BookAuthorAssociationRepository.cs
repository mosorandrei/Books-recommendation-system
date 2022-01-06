using Application.Interfaces;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.v2
{
    public class BookAuthorAssociationRepository : Repository<BookAuthorAssociation>, IBookAuthorAssociationRepository
    {
        public BookAuthorAssociationRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<ICollection<Guid>> GetAuthorsByBookId(Guid BookId)
        {
            ICollection<Guid> AuthorIds = new HashSet<Guid>();
            IEnumerable<BookAuthorAssociation> BookAuthorAssociations = await GetAllAsync();
            foreach (BookAuthorAssociation association in BookAuthorAssociations)
            {
                if (association.BookId == BookId)
                {
                    AuthorIds.Add(association.AuthorId);
                }
            }
            return AuthorIds;
        }

        public async Task<ICollection<Guid>> GetBooksByAuthorId(Guid AuthorId)
        {
            ICollection<Guid> BookIds = new HashSet<Guid>();
            IEnumerable<BookAuthorAssociation> BookAuthorAssociations = await GetAllAsync();
            foreach (BookAuthorAssociation association in BookAuthorAssociations)
            {
                if (association.AuthorId == AuthorId)
                {
                    BookIds.Add(association.BookId);
                }
            }
            return BookIds;
        }
    }
}
