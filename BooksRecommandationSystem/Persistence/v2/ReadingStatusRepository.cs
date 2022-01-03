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

        public async Task<string> AddToMyFavourites(string UserId, Guid BookId)
        {
            IEnumerable<ReadingStatus> ReadingStatuses = await GetAllAsync();
            foreach (ReadingStatus status in ReadingStatuses)
            {
                if (status.ApplicationUserId == UserId && status.BookId == BookId)
                {
                    if (status.IsFavourited)
                    {
                        return "The chosen Book is already in Favourites!";
                    }
                    status.IsFavourited = true;
                    await UpdateAsync(status);
                    return "The chosen Book is now in Favourites!";
                }
            }
            throw new NullReferenceException("No Entry found with the specified parameters!");
        }

        public async Task<string> DeleteFromMyFavourites(string UserId, Guid BookId)
        {
            IEnumerable<ReadingStatus> ReadingStatuses = await GetAllAsync();
            foreach (ReadingStatus status in ReadingStatuses)
            {
                if (status.ApplicationUserId == UserId && status.BookId == BookId)
                {
                    if (!status.IsFavourited)
                    {
                        return "The chosen Book is not in Favourites!";
                    }
                    status.IsFavourited = false;
                    await UpdateAsync(status);
                    return "The chosen Book is not in Favourites anymore!";
                }
            }
            throw new NullReferenceException("No Entry found with the specified parameters!");
        }

        public async Task<ICollection<Guid>> GetMyFavourites(string UserId)
        {
            ICollection<Guid> BookIds = new HashSet<Guid>();
            IEnumerable<ReadingStatus> ReadingStatuses = await GetAllAsync();
            foreach (ReadingStatus status in ReadingStatuses)
            {
                if (status.ApplicationUserId == UserId)
                {
                    if (status.IsFavourited)
                    {
                        BookIds.Add(status.BookId);
                    }
                }
            }
            return BookIds;
        }

        public async Task<ICollection<Guid>> GetMyToReads(string UserId)
        {
            ICollection<Guid> BookIds = new HashSet<Guid>();
            IEnumerable<ReadingStatus> ReadingStatuses = await GetAllAsync();
            foreach (ReadingStatus status in ReadingStatuses)
            {
                if (status.ApplicationUserId == UserId)
                {
                    if (status.Status == Domain.Constants.ReadingStatusEnum.ToBeReaded)
                    {
                        BookIds.Add(status.BookId);
                    }
                }
            }
            return BookIds;
        }

        public async Task<string> StartReading(string UserId, Guid BookId)
        {
            IEnumerable<ReadingStatus> ReadingStatuses = await GetAllAsync();
            foreach (ReadingStatus status in ReadingStatuses)
            {
                if (status.ApplicationUserId == UserId && status.BookId == BookId)
                {
                    if (status.Status != Domain.Constants.ReadingStatusEnum.ToBeReaded)
                    {
                        return "The chosen Book either started or finished!";
                    }
                    status.Status = Domain.Constants.ReadingStatusEnum.Reading;
                    await UpdateAsync(status);
                    return "You are now reading the chosen Book!";
                }
            }
            throw new NullReferenceException("No Entry found with the specified parameters!");
        }

        public async Task<ICollection<Guid>> GetMyReadings(string UserId)
        {
            ICollection<Guid> BookIds = new HashSet<Guid>();
            IEnumerable<ReadingStatus> ReadingStatuses = await GetAllAsync();
            foreach (ReadingStatus status in ReadingStatuses)
            {
                if (status.ApplicationUserId == UserId)
                {
                    if (status.Status == Domain.Constants.ReadingStatusEnum.Reading)
                    {
                        BookIds.Add(status.BookId);
                    }
                }
            }
            return BookIds;
        }

        public async Task<string> FinishReading(string UserId, Guid BookId)
        {
            IEnumerable<ReadingStatus> ReadingStatuses = await GetAllAsync();
            foreach (ReadingStatus status in ReadingStatuses)
            {
                if (status.ApplicationUserId == UserId && status.BookId == BookId)
                {
                    if (status.Status != Domain.Constants.ReadingStatusEnum.Reading)
                    {
                        return "The chosen Book either not started or already finished!";
                    }
                    status.Status = Domain.Constants.ReadingStatusEnum.Read;
                    await UpdateAsync(status);
                    return "You have now finished reading the chosen Book!";
                }
            }
            throw new NullReferenceException("No Entry found with the specified parameters!");
        }

        public async Task<ICollection<Guid>> GetMyReads(string UserId)
        {
            ICollection<Guid> BookIds = new HashSet<Guid>();
            IEnumerable<ReadingStatus> ReadingStatuses = await GetAllAsync();
            foreach (ReadingStatus status in ReadingStatuses)
            {
                if (status.ApplicationUserId == UserId)
                {
                    if (status.Status == Domain.Constants.ReadingStatusEnum.Read)
                    {
                        BookIds.Add(status.BookId);
                    }
                }
            }
            return BookIds;
        }

        public async Task<string> RateBook(string UserId, Guid BookId, int Score)
        {
            IEnumerable<ReadingStatus> ReadingStatuses = await GetAllAsync();
            foreach (ReadingStatus status in ReadingStatuses)
            {
                if (status.ApplicationUserId == UserId && status.BookId == BookId)
                {
                    if (status.UserScore != 0)
                    {
                        status.UserScore = Score;
                        await UpdateAsync(status);
                        return "Rating of chosen book has been UPDATED successfully!";
                    }
                    status.UserScore = Score;
                    await UpdateAsync(status);
                    return "Rating of chosen book has been ADDED successfully!";
                }
            }
            throw new NullReferenceException("No Entry found with the specified parameters!");
        }

        public async Task<int> GetUserAssignedScore(string UserId, Guid BookId)
        {
            IEnumerable<ReadingStatus> ReadingStatuses = await GetAllAsync();
            foreach (ReadingStatus status in ReadingStatuses)
            {
                if (status.ApplicationUserId == UserId && status.BookId == BookId)
                {
                    return status.UserScore;
                }
            }
            throw new NullReferenceException("No Entry found with the specified parameters!");
        }
    }
}
