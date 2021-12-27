using Application.Features.Queries;
using Application.Interfaces;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service.QueryHandlerTests
{
    public class GetStatusesQueryHandlerTests
    {
        private readonly GetStatusesQueryHandler handler;
        private readonly IReadingStatusRepository repository;

        public GetStatusesQueryHandlerTests()
        {
            repository = A.Fake<IReadingStatusRepository>();
            handler = new GetStatusesQueryHandler(repository);
        }

        [Fact]
        public async Task GivenStatusesQueryWhenHandleIsCalledThenGetAllAsyncIsCalled()
        {
            GetStatusesQuery query = new GetStatusesQuery();
            await handler.Handle(query, default);
            A.CallTo(() => repository.GetAllAsync()).MustHaveHappenedOnceExactly();

        }




    }
}
