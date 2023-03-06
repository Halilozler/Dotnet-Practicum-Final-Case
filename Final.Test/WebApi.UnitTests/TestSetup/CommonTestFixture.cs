using System;
using AutoMapper;
using Final.Data.Context;
using Final.Data.Repository.Mongo.Abstarct;
using Final.Data.Repository.Sql.Abstract;
using Final.Data.Repository.Sql.Concrete;
using Final.Data.UnitOfWork;
using Final.Dto.Dtos;
using Final.Dto.Dtos.Create;
using Final.Service.Abstract;
using Final.Service.Concrete;
using Final.Service.Mapper;
using Microsoft.EntityFrameworkCore;

namespace WebApi.UnitTests.TestSetup
{
	public class CommonTestFixture
	{
		public AppDbContext Context { get; set; }
		public IMapper Mapper { get; set; }
        public IUnitOfWork unitOfWork { get; set; }
        public IUserRepository userRepository { get; set; }
        public IRoleService roleService { get; set; }
        public IListsService listsService { get; set; }
        public IListItemService listItemService { get; set; }
		public IAgreeListRepository agreeListRepository { get; set; }
        public IRoleRepository roleRepository { get; set; }
        public IListsRepository listsRepository { get; set; }
        public IListItemRepository listItemRepository { get; set; }
        public IUserService<UserDto> userDtoService { get; set; }
        public IUserService<CreateUserDto> userCreateDtoService { get; set; }

        public CommonTestFixture()
		{
			var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "ListTestDB").Options;

			Context = new AppDbContext(options);
			Context.Database.EnsureCreated();

			//create Data
			Context.AddGenres();
			Context.AddRoles();

			//Mapper configure
			Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();

			userRepository = new UserRepository(Context);
			unitOfWork = new UnitOfWork(Context);
			roleRepository = new RoleRepository(Context);
			roleService = new RoleService(roleRepository, Mapper, unitOfWork);
			listsRepository = new ListsRepository(Context);
			agreeListRepository = null;

			userDtoService = new UserService<UserDto>(userRepository, Mapper, unitOfWork, roleService);
            userCreateDtoService = new UserService<CreateUserDto>(userRepository, Mapper, unitOfWork, roleService);

			listItemRepository = new ListItemRepository(Context);
			listItemService = new ListItemService(listItemRepository, listsRepository, Mapper, unitOfWork);

			listsService = new ListsService(listsRepository, userDtoService, Mapper, unitOfWork, listItemService, agreeListRepository);
        }
	}
}

