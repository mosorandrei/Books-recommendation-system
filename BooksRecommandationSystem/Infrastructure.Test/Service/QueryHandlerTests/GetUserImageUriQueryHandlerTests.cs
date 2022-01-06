using Application.Features.Queries;
using Application.Interfaces;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service.QueryHandlerTests
{
    public class GetUserImageUriQueryHandlerTests
    {
        private readonly GetUserImageUriQueryHandler handler;
        private readonly ITokenRepository repository;

        public GetUserImageUriQueryHandlerTests()
        {
            repository = A.Fake<ITokenRepository>();
            handler = new GetUserImageUriQueryHandler(repository);
        }

        [Fact]
        public async Task GivenGetUserImageUriQueryWhenHandleIsCalledThenGetUserByIdIsCalled()
        {
            GetUserImageUriQuery query = new GetUserImageUriQuery();
            query.UserId = "ID";
            await handler.Handle(query, default);
            A.CallTo(() => repository.GetUserById(A<string>._)).MustHaveHappenedOnceExactly();

        }

        [Fact]
        public void GivenGetUserImageUriQueryWhenHandleIsCalledAndUserIdIsNullThenShouldThrowException()
        {
            GetUserImageUriQuery query = new GetUserImageUriQuery();
            query.UserId = null;
            Func<Task> action = async () => await handler.Handle(query, default);

            _ = action.Should().ThrowAsync<ArgumentNullException>();

        }



    }
}
