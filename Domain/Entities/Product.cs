using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
	public class Product : BaseEntity
	{
        public string ProductName { get; set; }
        public string Manufacturer { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal CostPrice  { get; set; }
        public decimal SalesPrice { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Size { get; set; } = string.Empty;
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
    }
}
