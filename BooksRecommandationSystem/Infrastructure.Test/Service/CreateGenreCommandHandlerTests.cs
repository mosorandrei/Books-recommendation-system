using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entities;
using FakeItEasy;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service
{
    public class CreateGenreCommandHandlerTests
    {
        private readonly CreateGenreCommandHandler handler;
        private readonly IGenreRepository repository;

        public CreateGenreCommandHandlerTests()
        {
            this.repository = A.Fake<IGenreRepository>();
            this.handler = new CreateGenreCommandHandler(this.repository);
        }

        [Fact]
        public async Task Given_CreateGenreCommandHandler_When_HandleIsCalled_Then_AddAsyncGenreIsCalled()
        {
            await handler.Handle(new CreateGenreCommand(), default);
            A.CallTo(() => repository.AddAsync(A<Genre>._)).MustHaveHappenedOnceExactly();

        }
    }
}
