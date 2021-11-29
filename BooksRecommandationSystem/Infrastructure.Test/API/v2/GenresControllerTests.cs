
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
    public class GenresControllerTests
    {
        private readonly GenresController controller;
        private readonly IMediator mediator;

        public GenresControllerTests()
        {
            mediator = A.Fake<IMediator>();
            controller = new GenresController(mediator);
        }

        [Fact]
        public async Task Given_GenresController_When_GetIsCalled_Then_ShouldReturnAGenreCollection()
        {
            await controller.Get();
            A.CallTo(() => mediator.Send(A<GetGenresQuery>._, default)).MustHaveHappenedOnceExactly();

        }

        [Fact]
        public async Task Given_GenresController_When_CreateIsCalled_Then_ShouldCreateAGenre()
        {
            await controller.Create(new CreateGenreCommand());
            A.CallTo(() => mediator.Send(A<CreateGenreCommand>._, default)).MustHaveHappenedOnceExactly();

        }
        [Fact]
        public async Task Given_GenresController_When_UpdateIsCalled_Then_ShouldUpdateAGenre()
        {
            await controller.Update(new Guid(), new UpdateGenreCommand());
            A.CallTo(() => mediator.Send(A<UpdateGenreCommand>._, default)).MustHaveHappenedOnceExactly();

        }

        [Fact]
        public async Task Given_GenresController_When_DeleteIsCalled_Then_ShouldDeleteAGenre()
        {
            await controller.Delete(new Guid());
            A.CallTo(() => mediator.Send(A<DeleteGenreCommand>._, default)).MustHaveHappenedOnceExactly();

        }

    }
}
