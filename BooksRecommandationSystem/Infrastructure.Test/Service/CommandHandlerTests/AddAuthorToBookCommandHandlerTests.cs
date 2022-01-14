

using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entities;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service.CommandHandlerTests
{
    public class AddAuthorToBookCommandHandlerTests
    {
        private readonly AddAuthorToBookCommandHandler handler;
        private readonly IBookAuthorAssociationRepository repository;
        private readonly IBookRepository bookRepository;
        private readonly IAuthorRepository authorRepository;

        public AddAuthorToBookCommandHandlerTests()
        {
            repository = A.Fake<IBookAuthorAssociationRepository>();
            bookRepository = A.Fake<IBookRepository>();
            authorRepository = A.Fake<IAuthorRepository>();
            handler = new AddAuthorToBookCommandHandler(repository, bookRepository, authorRepository);
        }
        [Fact]
        public async Task GivenAddAuthorToBookCommandHandlerWhenHandleIsCalledThenAddAsyncAuthorIsCalled()
        {
            await handler.Handle(new AddAuthorToBookCommand(), default);
            A.CallTo(() => repository.AddAsync(A<BookAuthorAssociation>._)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public void GivenAddAuthorToBookCommandHandlerWhenHandleIsCalledAndBookIdIsNullThenShouldThrowArgumentNullException()
        {
            AddAuthorToBookCommand command = new()
            {
                BookId = Guid.Empty,
                AuthorId = new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052")
            };

            Func<Task> action = async () => await handler.Handle(command, default);

            _ = action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public void GivenAddAuthorToBookCommandHandlerWhenHandleIsCalledAndAuthorIdIsNullThenShouldThrowArgumentNullException()
        {
            AddAuthorToBookCommand command = new()
            {
                BookId = new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052"),
                AuthorId = Guid.Empty
            };

            Func<Task> action = async () => await handler.Handle(command, default);

            _ = action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GivenAddAuthorToBookCommandHandlerWhenHandleIsCalledAndAuthorIsAssociatedThenShouldReturnMessage()
        {
            AddAuthorToBookCommand command = new()
            {
                BookId = new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052"),
                AuthorId = new Guid("6a55c0c8-a75c-4a4a-9714-5a31e431e052")
            };
            BookAuthorAssociation association = new()
            {
                AuthorId = new Guid("6a55c0c8-a75c-4a4a-9714-5a31e431e052"),
                BookId = new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052")
                
            };
            IEnumerable<BookAuthorAssociation> associations = new[]
            {
                association
            };
            A.CallTo(() => repository.GetAllAsync()).Returns(associations);

            string result = await handler.Handle(command, default);

            result.Should().Be("Author already associated with book!");
        }
    }
}
