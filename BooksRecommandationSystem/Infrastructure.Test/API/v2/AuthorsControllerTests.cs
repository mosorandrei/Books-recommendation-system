
using System;
using System.Threading.Tasks;
using Application.Features.Commands;
using Application.Features.Queries;
using FakeItEasy;
using MediatR;
using WebAPI.Controllers.v2;
using Xunit;

namespace Infrastructure.Test.API.v2
{
    public class AuthorsControllerTests
    {
        private readonly AuthorsController controller;
        private readonly IMediator mediator;

        public AuthorsControllerTests()
        {
            mediator = A.Fake<IMediator>();
            controller = new AuthorsController(mediator);
        }

        [Fact]
        public async Task GivenAuthorsControllerWhenGetIsCalledThenShouldReturnAnAuthorCollection()
        {
            await controller.Get();
            A.CallTo(() => mediator.Send(A<GetAuthorsQuery>._, default)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GivenAuthorsControllerWhenCreateIsCalledThenShouldCreateAnAuthor()
        {
            await controller.Create(new CreateAuthorCommand());
            A.CallTo(() => mediator.Send(A<CreateAuthorCommand>._, default)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GivenAuthorsControllerWhenUpdateIsCalledThenShouldUpdateAnAuthor()
        {
            await controller.Update(new Guid(),new UpdateAuthorCommand());
            A.CallTo(() => mediator.Send(A<UpdateAuthorCommand>._, default)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Given_AuthorsController_When_DeleteIsCalled_Then_ShouldDeleteAnAuthor()
        {
            await controller.Delete(new Guid());
            A.CallTo(() => mediator.Send(A<DeleteAuthorCommand>._, default)).MustHaveHappenedOnceExactly();
        }
    }
}
