using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBookAuthorAssociationRepository : IRepository<BookAuthorAssociation>
    {
        Task<ICollection<Guid>> GetBooksByAuthorId(Guid GenreId);
        Task<ICollection<Guid>> GetAuthorsByBookId(Guid BookId);
    }
}
