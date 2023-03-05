using System;
using Final.Base.Model;

namespace Final.Dto.Dtos
{
	public class ListItemDto : IDto
	{
		public int Id { get; set; }
		public string ProductName { get; set; }
		public Boolean Receipt { get; set; }
		public int Amount { get; set; }
		public string TypeName { get; set; }
	}
}

