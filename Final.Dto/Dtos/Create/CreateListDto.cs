using Final.Base.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Dto.Dtos.Create
{
    public class CreateListDto : IDto
    {
        [Required]
        [StringLength(maximumLength:20, MinimumLength = 2)]
        public string Name { get; set; }

        public int UserId { get; set; }

        public DateTime CreateDate = DateTime.Now;

        [StringLength(maximumLength: 100)]
        public string Explain { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        public string CategoryName { get; set; }
    }
}
