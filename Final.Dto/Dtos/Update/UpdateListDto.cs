using Final.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Dto.Dtos
{
    public class UpdateListDto : IDto
    {
        public string Name { get; set; }
        public string Explain { get; set; }
        public string CategoryName { get; set; }
    }
}
