using System;
using Final.Base.Model;
using Final.Dto.Dtos;
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
        public DateTime CompletionDate = DateTime.Now;

        //One to Many Product
        public IEnumerable<ListItemDto> Items { get; set; }

        public int UserId { get; set; }
        public string Explain { get; set; }
        public string CategoryName { get; set; }
    }
}

