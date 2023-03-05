using System;
using AutoMapper;
using Final.Data.Model.DatabaseSql;
using Final.Dto.Dtos;
using Final.Dto.Dtos.Create;

namespace Final.Service.Mapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<CreateUserDto, User>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<RoleDto, Role>().ReverseMap();
			CreateMap<ListsDto, Lists>().ReverseMap();

			CreateMap<ListItem, ListItemDto>().ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Genre.Name));
			CreateMap<UpdateListDto, Lists>().ReverseMap();
			CreateMap<CreateListDto, Lists>().ReverseMap();

			CreateMap<CreateListItemDto, ListItem>().ReverseMap();
            CreateMap<UpdateListItemDto, ListItem>().ReverseMap();
        }
	}
}

