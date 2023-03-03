using System;
using Final.Base.Model;

namespace Final.Data.Repository.Mongo.Abstarct
{
	public interface IMongoRepository<TEntity> where TEntity : class, IEntity
	{
	}
}

