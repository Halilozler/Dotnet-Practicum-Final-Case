using System;
using Final.Base.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Final.Data.Model.DatabaseMongo
{
	public class Product : IEntity
	{
        public string Name { get; set; }
        public int Amount { get; set; }
        public string TypeName { get; set; }
    }
}

