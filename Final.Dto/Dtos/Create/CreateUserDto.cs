using Final.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Dto.Dtos.Create
{
    public class CreateUserDto : IDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
