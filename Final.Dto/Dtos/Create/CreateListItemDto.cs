using Final.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Dto.Dtos.Create
{
    public class CreateListItemDto : IDto
    {
        public int ListsId { get; set; }
        public string ProductName { get; set; }
        public bool Receipt = false;
        public int Amount { get; set; }
        public int GenreId { get; set; }
    }
}
