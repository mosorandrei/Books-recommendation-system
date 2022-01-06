using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetStatusesQueryHandler : IRequestHandler<GetStatusesQuery, IEnumerable<ReadingStatus>>
    {
        private readonly IReadingStatusRepository repository;

        public GetStatusesQueryHandler(IReadingStatusRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IEnumerable<ReadingStatus>> Handle(GetStatusesQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAllAsync();
        }
    }
}