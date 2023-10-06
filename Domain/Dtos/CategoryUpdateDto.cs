using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
	public class CategoryUpdateDto
	{
		public string CategoryName { get; set; }
		public string Description { get; set; } = string.Empty;
	}
}
