using System;
using System.ComponentModel.DataAnnotations;
using Final.Base.Attribute;
using Final.Base.Model;

namespace Final.Dto.Dtos
{
	public class UserDto : IDto
	{
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        //[PasswordAttribute]
        public string Password { get; set; }
	}
}

