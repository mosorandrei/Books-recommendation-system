using Application.Features.Queries;
using Application.Interfaces;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service.QueryHandlerTests
{
    public class GetUserIsBlockedQueryHandlerTests
    {
        private readonly GetUserIsBlockedQueryHandler handler;
        private readonly ITokenRepository repository;

        public GetUserIsBlockedQueryHandlerTests()
        {
            repository = A.Fake<ITokenRepository>();
            handler = new GetUserIsBlockedQueryHandler(repository);
        }

        [Fact]

        public async void GivenGetUserIsBlockedQueryHandlerWhenHandleIsCalledThenGetTheUserById()
        { 
            GetUserIsBlockedQuery query = new GetUserIsBlockedQuery();
            query.UserId = "user";
            await handler.Handle(query, default);
            A.CallTo(() => repository.GetUserById(query.UserId)).MustHaveHappenedOnceExactly();
            
        }

        [Fact]

        public void GivenGetUserIsBlockedQueryHandlerWhenHandleIsCalledAndUserIdIsNullThenShouldThrowException()
        {
            GetUserIsBlockedQuery query = new GetUserIsBlockedQuery();
            query.UserId = null;
            Func<Task> action = async () => await handler.Handle(query, default);

            _ = action.Should().ThrowAsync<ArgumentNullException>();

        }
    }
}
