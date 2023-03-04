using System;
using Final.Base.Model;
using Final.Data.Model.DatabaseSql;

namespace Final.Service.Abstract
{
	public interface IListsService<ListsDto> : IBaseService<ListsDto, ListItem> where ListsDto : IDto
    {
	}
}

