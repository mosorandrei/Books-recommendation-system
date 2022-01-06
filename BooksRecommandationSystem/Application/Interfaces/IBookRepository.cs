using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<string> UpdateBookReviews(Guid BookId, int PreviousScore, int Score);
    }
}
