using Application.Features.Queries;
using Application.Interfaces;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service.QueryHandlerTests
{
    public class GetMyReadingsQueryHandlerTests
    {
        private readonly GetMyReadingsQueryHandler handler;
        private readonly IReadingStatusRepository repository;
        private readonly IBookRepository bookRepository;

        public GetMyReadingsQueryHandlerTests()
        {
            repository = A.Fake<IReadingStatusRepository>();
            bookRepository = A.Fake<IBookRepository>();
            handler = new GetMyReadingsQueryHandler(repository, bookRepository);
        }

        [Fact]
        public async Task GivenGetMyReadingsQueryWhenHandleIsCalledThenGetMyReadingsIsCalled()
        {
            GetMyReadingsQuery query = new GetMyReadingsQuery();
            query.UserId = "ID";
            await handler.Handle(query, default);
            A.CallTo(() => repository.GetMyReadings(A<string>._)).MustHaveHappenedOnceExactly();

        }

        [Fact]
        public void GivenGetMyReadingsQueryWhenHandleIsCalledAndUserIdIsNullThenShouldThrowException()
        {
            GetMyReadingsQuery query = new GetMyReadingsQuery();
            query.UserId = null;
            Func<Task> action = async () => await handler.Handle(query, default);

            _ = action.Should().ThrowAsync<ArgumentNullException>();

        }



    }
}
