using System;
using AutoMapper;
using Final.Data.Context;
using Final.Data.Model.DatabaseSql;
using Final.Service.Abstract;
using FluentAssertions;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application
{
	public class GetListCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IListsService _service;

        public GetListCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
            _service = testFixture.listsService;
        }

        [Fact]
        public void WhenListIdIsNotFound_InvalidOperationException_ShouldBeReturn()
        {
            //arrange   ********************
            int listId = 34242432;

            //act &	assert	********************
            FluentActions
                .Invoking(async () => await _service.GetByListIdTurnAgreeListAsync(listId, null))
                .Should().ThrowAsync<InvalidOperationException>();
        }
    }
}

