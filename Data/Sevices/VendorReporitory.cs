using Data.DataContext;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Sevices
{
	public class VendorReporitory : GenericRepository<Vendor>, IVendorRepository
	{
		public VendorReporitory(AppDbContext context) : base(context)
		{
		}

		public async Task<Vendor?> GetVendorByName(string vendorName)
		{
			return await _context.Vendors.FirstOrDefaultAsync(v => v.VendorName == vendorName);
		}
	}
}
