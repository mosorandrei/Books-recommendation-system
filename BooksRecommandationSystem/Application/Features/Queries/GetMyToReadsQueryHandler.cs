﻿using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetMyToReadsQueryHandler : IRequestHandler<GetMyToReadsQuery, IEnumerable<Book>>
    {
        private readonly IReadingStatusRepository repository;
        private readonly IBookRepository bookRepository;

        public GetMyToReadsQueryHandler(IReadingStatusRepository repository, IBookRepository bookRepository)
        {
            this.repository = repository;
            this.bookRepository = bookRepository;
        }
        public async Task<IEnumerable<Book>> Handle(GetMyToReadsQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
                throw new ArgumentNullException(request.UserId);
            ICollection<Guid> BookIds = await repository.GetMyToReads(request.UserId);
            List<Book> FetchedBooks = new();
            foreach (Guid guid in BookIds)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                Book CurrentBook = await bookRepository.GetByIdAsync(guid);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                if (CurrentBook == null)
                    throw new NullReferenceException(nameof(CurrentBook));
                FetchedBooks.Add(CurrentBook);
            }
            return FetchedBooks;
        }
    }
}
