using System;
using System.ComponentModel.DataAnnotations.Schema;
using Final.Base.Model;

namespace Final.Data.Model.DatabaseSql
{
	public class User : IEntity
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}

