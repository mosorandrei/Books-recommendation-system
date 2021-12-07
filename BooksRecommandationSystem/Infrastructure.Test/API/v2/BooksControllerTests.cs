
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
    public class BooksControllerTests
    {
        private readonly BooksController controller;
        private readonly IMediator mediator;

        public BooksControllerTests()
        {
            mediator = A.Fake<IMediator>();
            controller = new BooksController(mediator);
        }

        [Fact]
        public async Task GivenBooksControllerWhenGetIsCalledThenShouldReturnABookCollection()
        {
            await controller.Get();
            A.CallTo(() => mediator.Send(A<GetBooksQuery>._, default)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Given_BooksController_When_CreateIsCalled_Then_ShouldCreateABook()
        {
            await controller.Create(new CreateBookCommand());
            A.CallTo(() => mediator.Send(A<CreateBookCommand>._, default)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Given_BooksController_When_UpdateIsCalled_Then_ShouldUpdateABook()
        {
            await controller.Update(new Guid(), new UpdateBookCommand());
            A.CallTo(() => mediator.Send(A<UpdateBookCommand>._, default)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Given_BooksController_When_DeleteIsCalled_Then_ShouldDeleteABook()
        {
            await controller.Delete(new Guid());
            A.CallTo(() => mediator.Send(A<DeleteBookCommand>._, default)).MustHaveHappenedOnceExactly();
        }
    }
}

