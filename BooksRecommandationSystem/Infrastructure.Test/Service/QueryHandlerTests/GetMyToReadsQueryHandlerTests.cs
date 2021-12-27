using Application.Features.Queries;
using Application.Interfaces;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service.QueryHandlerTests
{
    public class GetMyToReadsQueryHandlerTests
    {
        private readonly GetMyToReadsQueryHandler handler;
        private readonly IReadingStatusRepository repository;
        private readonly IBookRepository bookRepository;

        public GetMyToReadsQueryHandlerTests()
        {
            repository = A.Fake<IReadingStatusRepository>();
            bookRepository = A.Fake<IBookRepository>();
            handler = new GetMyToReadsQueryHandler(repository, bookRepository);
        }

        [Fact]
        public async Task GivenGetMyToReadsQueryWhenHandleIsCalledThenGetMyToReadsIsCalled()
        {
            GetMyToReadsQuery query = new GetMyToReadsQuery();
            query.UserId = "ID";
            await handler.Handle(query, default);
            A.CallTo(() => repository.GetMyToReads(A<string>._)).MustHaveHappenedOnceExactly();

        }

        [Fact]
        public void GivenGetMyToReadsQueryWhenHandleIsCalledAndUserIdIsNullThenShouldThrowException()
        {
            GetMyToReadsQuery query = new GetMyToReadsQuery();
            query.UserId = null;
            Func<Task> action = async () => await handler.Handle(query, default);

            _ = action.Should().ThrowAsync<ArgumentNullException>();

        }



    }
}
