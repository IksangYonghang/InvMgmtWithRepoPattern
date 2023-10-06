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
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{
		public ProductRepository(AppDbContext context) : base(context)
		{
		}

		public async Task<Product?> GetProductByName(string productName)
		{
			return await _context.Products.FirstOrDefaultAsync(p=>p.ProductName == productName);
		}
	}
}
