using System;
using System.ComponentModel.DataAnnotations.Schema;
using Final.Base.Model;

namespace Final.Data.Model.DatabaseSql
{
	public class User : IEntity
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR(20)")]
        public string Name { get; set; }
        [Column(TypeName = "VARCHAR(20)")]
        public string Surname { get; set; }
        public string Password { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}

