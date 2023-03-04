using System;
using AutoMapper;
using Final.Base.Model;
using Final.Data.Model.DatabaseSql;
using Final.Data.Repository.Sql.Abstract;
using Final.Data.UnitOfWork;
using Final.Service.Abstract;

namespace Final.Service.Concrete
{
	public class ListItemService<ListItemDto> : BaseService<ListItemDto, ListItem>, IListItemService<ListItemDto> where ListItemDto : IDto
    {
        private readonly IGenericRepository<ListItem> genericRepository;

        public ListItemService(IGenericRepository<ListItem> genericRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(genericRepository, mapper, unitOfWork)
        {
        }
    }
}

