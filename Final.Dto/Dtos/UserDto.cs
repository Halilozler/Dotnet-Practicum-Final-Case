using System;
using Final.Base.Model;

namespace Final.Dto.Dtos
{
	public class UserDto : IDto
	{
		public string Name { get; set; }
		public string Password { get; set; }
	}
}

