using System;
namespace Final.Data.Context
{
	public interface IMongoDbSettings
	{
		public string AgreeListCollectionName { get; set; }
		public string ProductCollectionName { get; set; }
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
	}
}

