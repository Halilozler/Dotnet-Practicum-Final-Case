using System;
using AutoMapper;
using Final.Data.Model.DatabaseSql;
using Final.Dto.Dtos.Create;

namespace Final.Service.Mapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<CreateUserDto, User>().ReverseMap();
		}
	}
}

