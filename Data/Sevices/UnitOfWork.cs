using Data.DataContext;
using Data.Sevices;
using Models.Entities;
using Models.Repositories;
using System;
using System.Threading.Tasks;

namespace Data.Services
{
	public class UnitOfWork : IUnitOfWork
	{
		protected readonly AppDbContext _context;

		public UnitOfWork(AppDbContext context)
		{
			_context = context;
			Category = new GenericRepository<Category>(_context);
			Product = new GenericRepository<Product>(_context);
			Vendor = new GenericRepository<Vendor>(_context);
		}

		public IGenericRepository<Category> Category { get; private set; }
		public IGenericRepository<Product> Product { get; private set; }
		public IGenericRepository<Vendor> Vendor { get; private set; }

		public void Dispose()
		{
			_context.Dispose();
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
