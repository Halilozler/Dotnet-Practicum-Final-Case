using System;
using Final.Base.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Final.Data.Model.DatabaseMongo
{
	public class AgreeList : IEntity
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedDate { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CompletionDate { get; set; }

        //One to Many Product
        public IList<Product> Products { get; }

        public int UserId { get; set; }
        public string Explain { get; set; }
        public int CategoryId { get; set; }
    }
}

