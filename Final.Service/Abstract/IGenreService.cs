using System;
using Final.Base.Model;
using Final.Data.Model.DatabaseSql;
using Final.Dto.Dtos;

namespace Final.Service.Abstract
{
	public interface IGenreService : IBaseService<GenreDto, Genre>
	{
	}
}

