using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Repositories
{
	public interface IVendorRepository
	{
		Task<Vendor?> GetVendorByName(string vendorName);
	}
}
