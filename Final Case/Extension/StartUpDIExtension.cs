using System;
using AutoMapper;
using Final.Data.LoggerService;
using Final.Data.Model.DatabaseSql;
using Final.Data.Repository.Sql.Abstract;
using Final.Data.Repository.Sql.Concrete;
using Final.Data.UnitOfWork;
using Final.Dto.Dtos;
using Final.Dto.Dtos.Create;
using Final.Service.Abstract;
using Final.Service.Concrete;
using Final.Service.Mapper;

namespace Final_Case.Extension
{
	public static class StartUpDIExtension
	{
        public static void AddServiceDI(this IServiceCollection services)
        {
            //Dependency Injection
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<ILoggerService, SerialLogger>();

            //Repository
            services.AddScoped<IGenericRepository<Lists>, GenericRepository<Lists>>();
            //services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            services.AddScoped<IGenericRepository<ListItem>, GenericRepository<ListItem>>();
            services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
            services.AddScoped<IGenericRepository<Role>, GenericRepository<Role>>();
            services.AddScoped<IGenericRepository<Genre>, GenericRepository<Genre>>();

            services.AddScoped<IUserRepository, UserRepository>();

            //Service
            services.AddScoped<IListsService<CreateListDto>, ListsService<CreateListDto>>();
            services.AddScoped<IListsService<UpdateListDto>, ListsService<UpdateListDto>>();

            services.AddScoped<IListItemService<CreateListItemDto>, ListItemService<CreateListItemDto>>();
            services.AddScoped<IListItemService<UpdateListItemDto>, ListItemService<UpdateListItemDto>>();

            services.AddScoped<IUserService<CreateUserDto>, UserService<CreateUserDto>>();
            services.AddScoped<IUserService<UserDto>, UserService<UserDto>>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IGenreService, GenreService>();

            //Mapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());
        }
	}
}

