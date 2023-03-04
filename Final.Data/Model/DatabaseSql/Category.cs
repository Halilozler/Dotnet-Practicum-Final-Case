using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Final.Base.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Final.Data.Model.DatabaseSql
{
	public class Category : IEntity
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //[Required]
        [StringLength(maximumLength: 20, MinimumLength = 2)]
        [Column(TypeName = "VARCHAR(20)")]
        public string Name { get; set; }
    }
}

