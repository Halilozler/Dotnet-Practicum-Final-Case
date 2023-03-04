using Final.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Dto.Dtos
{
    public class UpdateListItemDto : IDto
    {
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public int TypeId { get; set; }
    }
}
