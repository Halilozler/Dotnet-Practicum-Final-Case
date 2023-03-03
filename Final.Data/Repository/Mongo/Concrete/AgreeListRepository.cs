using System;
using Final.Base.Model;
using Final.Data.Context;
using Final.Data.Model.DatabaseMongo;
using Final.Data.Repository.Mongo.Abstarct;
using MongoDB.Driver;

namespace Final.Data.Repository.Mongo.Concrete
{
	public class AgreeListRepository : MongoRepository<AgreeList, AgreeList>
    {
        public AgreeListRepository(IMongoCollection<AgreeList> collection) : base(collection)
        {
            /*
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _collection = database.GetCollection<AgreeList>(databaseSettings.AgreeListCollectionName);
            */
        }
    }
}

