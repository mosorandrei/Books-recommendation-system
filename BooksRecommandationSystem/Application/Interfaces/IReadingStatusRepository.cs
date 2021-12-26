using Domain.Entities;

namespace Application.Interfaces
{
    public interface IReadingStatusRepository : IRepository<ReadingStatus>
    {
        Task<string> AddToMyFavourites(string UserId, Guid BookId);
        Task<string> DeleteFromMyFavourites(string UserId, Guid BookId);
        Task<ICollection<Guid>> GetMyFavourites(string UserId);
        Task<ICollection<Guid>> GetMyToReads(string UserId);
        Task<string> StartReading(string UserId, Guid BookId);
        Task<ICollection<Guid>> GetMyReadings(string UserId);
        Task<string> FinishReading(string UserId, Guid BookId);
        Task<ICollection<Guid>> GetMyReads(string UserId);
    }
}
