using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
	public class ProductUpdateDto
	{
		public string ProductName { get; set; }
		public string Manufacturer { get; set; }
		public string Description { get; set; }
		public decimal CostPrice { get; set; }
		public decimal SalesPrice { get; set; }
		public int CategoryId { get; set; }
		public string Size { get; set; }
		public int VendorId { get; set; }
	}
}
