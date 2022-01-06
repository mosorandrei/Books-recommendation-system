using Application.Interfaces;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.v2
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<string> UpdateBookReviews(Guid BookId, int PreviousScore, int Score)
        {
            Book? book = await GetByIdAsync(BookId);
            if (book == null)
                throw new NullReferenceException("No Book found with the specified ID!");
            book.NumberOfReviews++;
            book.Rating = ((book.NumberOfReviews - 1) * book.Rating - PreviousScore + Score) / book.NumberOfReviews;
            await UpdateAsync(book);
            return "Book Rating updated successfully!";
        }
    }
}
