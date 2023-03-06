using System;
using AutoMapper;
using Final.Data.Context;
using Final.Data.Model.DatabaseSql;
using Final.Dto.Dtos;
using Final.Dto.Dtos.Create;
using Final.Service.Abstract;
using Final.Service.Concrete;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application
{
	public class CreateUserCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService<CreateUserDto> _service;

        public CreateUserCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;

            _service = testFixture.userCreateDtoService;
        }

        [Fact]
        public void WhenAlreadyExsistUserNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange   ********************
            var user = new User
            {
                Name = "halil",
                Surname = "ozler",
                Password = "1234",
                RoleId = 1
            };
            _context.User.Add(user);
            _context.SaveChanges();

            //act &	assert	********************
            FluentActions
                .Invoking(async () => await _service.InsertAsync(new CreateUserDto
                {
                    Name = "halil",
                    Surname = "ozler",
                    Password = "1234",
                    RoleId = 1
                }
            ))
                .Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public void HappyTest()
        {
            //act &		********************
            FluentActions
                .Invoking(async () => await _service.InsertAsync(new CreateUserDto
                {
                    Name = "halil123",
                    Surname = "ozler",
                    Password = "1234",
                    RoleId = 1
                }
            )).Invoke();

            //assert
            var user = _service.Login(new UserDto { Name = "halil123", Password = "1234" });
            user.Should().NotBeNull();
        }
    }
}

