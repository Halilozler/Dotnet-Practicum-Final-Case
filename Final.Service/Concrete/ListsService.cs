using System;
using AutoMapper;
using Final.Base.Model;
using Final.Data.Model.DatabaseSql;
using Final.Data.Repository.Sql.Abstract;
using Final.Data.UnitOfWork;
using Final.Service.Abstract;

namespace Final.Service.Concrete
{
	public class ListsService<ListsDto> : BaseService<ListsDto, Lists>, IListsService<ListsDto> where ListsDto : IDto
    {
        private readonly IGenericRepository<Lists> _genericRepository;

        public ListsService(IGenericRepository<Lists> genericRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(genericRepository, mapper, unitOfWork)
        {
        }
    }
}

