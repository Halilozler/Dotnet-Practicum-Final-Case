using System;
using System.ComponentModel.DataAnnotations.Schema;
using Final.Base.Model;

namespace Final.Data.Model.DatabaseSql
{
	public class Lists : IEntity
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
		public string Name { get; set; }
		public DateTime CreateDate { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
		public string Explain { get; set; }
		public int CategoryId { get; set; }
	}
}

