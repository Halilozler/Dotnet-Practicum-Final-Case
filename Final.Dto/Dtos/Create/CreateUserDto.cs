using Final.Base.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Dto.Dtos.Create
{
    public class CreateUserDto : IDto
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 2, ErrorMessage = "Invalid Name Lenght")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 2, ErrorMessage = "Invalid Surname Lenght")]
        public string Surname { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 5, ErrorMessage = "Invalid Password Lenght")]
        public string Password { get; set; }
        [Required]
        public int RoleId { get; set; }
    }
}
