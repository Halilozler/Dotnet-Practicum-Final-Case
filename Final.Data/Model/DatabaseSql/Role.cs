using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Final.Base.Model;

namespace Final.Data.Model.DatabaseSql
{
    //Admin, Normal
	public class Role : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //[Required]
        [StringLength(maximumLength: 20, MinimumLength = 2)]
        [Column(TypeName = "VARCHAR(20)")]
        public string Name { get; set; }
    }
}

