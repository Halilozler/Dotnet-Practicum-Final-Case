using System;
using Final.Base.Model;
using Final.Data.Model.DatabaseSql;

namespace Final.Service.Abstract
{
	public interface IListItemService<ListItemDto> : IBaseService<ListItemDto, ListItem> where ListItemDto : IDto
	{
	}
}

