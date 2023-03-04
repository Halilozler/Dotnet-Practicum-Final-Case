using System;
using System.ComponentModel.DataAnnotations.Schema;
using Final.Base.Model;

namespace Final.Data.Model.DatabaseSql
{
	public class ListItem : IEntity
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR(20)")]
        public string ProductName { get; set; }

        public int ListsId { get; set; }
        public Lists Lists { get; set; }

        public Boolean Receipt { get; set; }
        [Column(TypeName = "SMALLINT")]
        public int Amount { get; set; }
        public int TaypeId { get; set; }
    }
}

