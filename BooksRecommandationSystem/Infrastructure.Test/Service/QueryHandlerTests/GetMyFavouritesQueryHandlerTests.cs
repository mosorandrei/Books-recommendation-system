using Application.Features.Queries;
using Application.Interfaces;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service.QueryHandlerTests
{
    public class GetMyFavouritesQueryHandlerTests
    {
        private readonly GetMyFavouritesQueryHandler handler;
        private readonly IReadingStatusRepository repository;
        private readonly IBookRepository bookRepository;

        public GetMyFavouritesQueryHandlerTests()
        {
            repository = A.Fake<IReadingStatusRepository>();
            bookRepository = A.Fake<IBookRepository>(); 
            handler = new GetMyFavouritesQueryHandler(repository,bookRepository);
        }

        [Fact]
        public async Task GivenGetMyFavouritesQueryWhenHandleIsCalledThenGetMyFavouritesIsCalled()
        {   GetMyFavouritesQuery query = new GetMyFavouritesQuery();
            query.UserId = "ID";
            await handler.Handle(query, default);
            A.CallTo(() => repository.GetMyFavourites(A<string>._)).MustHaveHappenedOnceExactly();
        
        }

        [Fact]
        public void GivenGetMyFavouritesQueryWhenHandleIsCalledAndUserIdIsNullThenShouldThrowException()
        {
            GetMyFavouritesQuery query = new GetMyFavouritesQuery();
            query.UserId = null;
            Func<Task> action = async () => await handler.Handle(query, default);

            _ = action.Should().ThrowAsync<ArgumentNullException>();

        }



    }
}
