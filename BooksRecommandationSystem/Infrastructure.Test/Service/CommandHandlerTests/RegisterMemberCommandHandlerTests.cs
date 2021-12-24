

using System.Threading.Tasks;
using Application.Features.Commands;
using Application.Interfaces;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Infrastructure.Test.Service
{
    public class RegisterMemberCommandHandlerTests
    {
        private readonly RegisterMemberCommandHandler handler;
        private readonly ITokenRepository repository;
        private readonly IBookRepository bookRepository;
        private readonly IReadingStatusRepository statusRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public RegisterMemberCommandHandlerTests()
        {
            this.repository = A.Fake<ITokenRepository>();
            bookRepository = A.Fake<IBookRepository>();
            statusRepository = A.Fake<IReadingStatusRepository>();
            this.httpContextAccessor = A.Fake<IHttpContextAccessor>();
            this.handler = new RegisterMemberCommandHandler(this.repository, this.httpContextAccessor, bookRepository, statusRepository);
        }
        [Fact]
        public async Task GivenRegisterMemberCommandWhenHandleIsCalledThenShouldRegisterTheUser()
        {
            await handler.Handle(new RegisterMemberCommand(), default);
            A.CallTo(() => repository.RegisterMember(A<RegisterMemberCommand>._)).MustHaveHappenedOnceExactly();
        }
    }
}
