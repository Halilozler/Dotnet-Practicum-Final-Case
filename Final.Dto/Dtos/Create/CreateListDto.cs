using Final.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Dto.Dtos.Create
{
    public class CreateListDto : IDto
    {
        public string Name { get; set; }

        public DateTime CreateDate = DateTime.Now;
        public string Explain { get; set; }
        public int CategoryId { get; set; }
    }
}
