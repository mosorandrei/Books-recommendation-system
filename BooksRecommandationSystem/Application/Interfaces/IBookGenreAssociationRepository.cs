using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBookGenreAssociationRepository : IRepository<BookGenreAssociation>
    {
        Task<ICollection<Guid>> GetBooksByGenreId(Guid GenreId);
        Task<ICollection<Guid>> GetGenresByBookId(Guid BookId);
    }
}
