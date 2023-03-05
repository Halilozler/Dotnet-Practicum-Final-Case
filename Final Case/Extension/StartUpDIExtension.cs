using System;
using AutoMapper;
using Final.Base.Services;
using Final.Data.LoggerService;
using Final.Data.Model.DatabaseSql;
using Final.Data.Repository.Mongo.Abstarct;
using Final.Data.Repository.Mongo.Concrete;
using Final.Data.Repository.Sql.Abstract;
using Final.Data.Repository.Sql.Concrete;
using Final.Data.UnitOfWork;
using Final.Dto.Dtos;
using Final.Dto.Dtos.Create;
using Final.Service.Abstract;
using Final.Service.Concrete;
using Final.Service.Mapper;
using Final.Service.TokenOperations.Abstract;
using Final.Service.TokenOperations.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace Final_Case.Extension
{
    public static class StartUpDIExtension
    {
        public static void AddServiceDI(this IServiceCollection services)
        {
            //Dependency Injection
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<ILoggerService, SerialLogger>();
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddScoped<IAgreeListRepository, AgreeListRepository>();

            //Repository
            //services.AddScoped<IGenericRepository<Lists>, GenericRepository<Lists>>();
            //services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            //services.AddScoped<IGenericRepository<ListItem>, GenericRepository<ListItem>>();
            //services.AddScoped<IGenericRepository<Role>, GenericRepository<Role>>();
            //services.AddScoped<IGenericRepository<Genre>, GenericRepository<Genre>>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IListsRepository, ListsRepository>();
            services.AddScoped<IListItemRepository, ListItemRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();

            //Service
            services.AddScoped<IListsService, ListsService>();

            services.AddScoped<IListItemService, ListItemService>();

            services.AddScoped<IUserService<CreateUserDto>, UserService<CreateUserDto>>();
            services.AddScoped<IUserService<UserDto>, UserService<UserDto>>();

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IGenreService, GenreService>();

            //Token
            services.AddScoped<ITokenManagementService, TokenManagementService>();

            //Mapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());
        }

        public static void AddCustomizeSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "List Appi Management", Version = "v1.0" });
                c.OperationFilter<ExtensionSwaggerFileOperationFilter>();
                //File eklenebilmesi için.
                //ExtensionSwaggerFileOperationFilter
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Techa Management for IT Company",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // Must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
            });
        }
    }
}

