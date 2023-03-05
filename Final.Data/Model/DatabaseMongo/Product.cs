using System;
using Final.Base.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Final.Data.Model.DatabaseMongo
{
	public class Product : IEntity
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string TypeName { get; set; }
    }
}

