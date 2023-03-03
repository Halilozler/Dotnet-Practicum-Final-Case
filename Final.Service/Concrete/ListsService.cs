using System;
using Final.Data.Model.DatabaseSql;
using Final.Data.Repository.Sql.Abstract;
using Final.Data.UnitOfWork;
using Final.Service.Abstract;

namespace Final.Service.Concrete
{
	public class ListsService : BaseService<Lists, Lists>, IListsService
	{
        private readonly IGenericRepository<Lists> _genericRepository;
        //Mapperda eklenicek

        public ListsService(IGenericRepository<Lists> genericRepository, IUnitOfWork unitOfWork) : base(genericRepository, unitOfWork)
        {
            _genericRepository = genericRepository;
        }
    }
}

