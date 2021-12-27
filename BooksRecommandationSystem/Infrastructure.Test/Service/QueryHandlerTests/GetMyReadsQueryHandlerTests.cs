using Application.Features.Queries;
using Application.Interfaces;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service.QueryHandlerTests
{
    public class GetMyReadsQueryHandlerTests
    {
        private readonly GetMyReadsQueryHandler handler;
        private readonly IReadingStatusRepository repository;
        private readonly IBookRepository bookRepository;

        public GetMyReadsQueryHandlerTests()
        {
            repository = A.Fake<IReadingStatusRepository>();
            bookRepository = A.Fake<IBookRepository>();
            handler = new GetMyReadsQueryHandler(repository, bookRepository);
        }

        [Fact]
        public async Task GivenGetMyReadsQueryWhenHandleIsCalledThenGetMyReadsIsCalled()
        {
            GetMyReadsQuery query = new GetMyReadsQuery();
            query.UserId = "ID";
            await handler.Handle(query, default);
            A.CallTo(() => repository.GetMyReads(A<string>._)).MustHaveHappenedOnceExactly();

        }

        [Fact]
        public void GivenGetMyReadsQueryWhenHandleIsCalledAndUserIdIsNullThenShouldThrowException()
        {
            GetMyReadsQuery query = new GetMyReadsQuery();
            query.UserId = null;
            Func<Task> action = async () => await handler.Handle(query, default);

            _ = action.Should().ThrowAsync<ArgumentNullException>();

        }



    }
}
