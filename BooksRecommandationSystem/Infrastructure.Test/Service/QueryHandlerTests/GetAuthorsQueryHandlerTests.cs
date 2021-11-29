﻿using Application.Features.Queries;
using Application.Interfaces;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service.QueryHandlerTests
{
    public class GetAuthorsQueryHandlerTests
    {
        private readonly GetAuthorsQueryHandler handler;
        private readonly IAuthorRepository repository;

        public GetAuthorsQueryHandlerTests()
        {
            this.repository = A.Fake<IAuthorRepository>();
            this.handler = new GetAuthorsQueryHandler(this.repository);
        }

        [Fact]
        public async void GivenGetAuthorsQueryHandler_WhenHandleIsCalled_ThenGetAllAsyncIsCalled()
        {
            await handler.Handle(new GetAuthorsQuery(), default);
            A.CallTo(() => repository.GetAllAsync()).MustHaveHappenedOnceExactly();
        }
    }
}
