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
	public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
	{
		public CategoryRepository(AppDbContext context) : base(context)
		{
		}

		public async Task<Category?> GetCategoryByName(string categoryName)
		{
			return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == categoryName);
		}
	}
}
