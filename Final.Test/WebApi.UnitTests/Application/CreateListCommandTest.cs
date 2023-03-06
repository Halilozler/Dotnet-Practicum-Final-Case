using System;
using AutoMapper;
using Final.Data.Context;
using Final.Data.Model.DatabaseSql;
using Final.Service.Abstract;
using Final.Service.Concrete;
using FluentAssertions;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application
{
	public class CreateListCommandTest : IClassFixture<CommonTestFixture>
	{
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IListsService _service;

        public CreateListCommandTest(CommonTestFixture testFixture)
		{
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;

            _service = testFixture.listsService;
        }

        [Fact]
        public void WhenUserIdIsNotFound_InvalidOperationException_ShouldBeReturn()
        {
            //arrange   ********************
            int userId = 328253423;

            //act &	assert	(çalıştırma & Doğrulama) ********************
            FluentActions
                .Invoking(async () => await _service.GetByIdAsync(userId))
                .Should().ThrowAsync<InvalidOperationException>();
            
        }
    }
}

