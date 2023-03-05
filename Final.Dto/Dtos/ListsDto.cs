using System;
using Final.Base.Model;

namespace Final.Dto.Dtos
{
	public class ListsDto : IDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime CreateDate { get; set; }
		public string Explain { get; set; }
		public string CategoryName { get; set; }

		public IEnumerable<ListItemDto> Items { get; set; }
	}
}

