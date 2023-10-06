using Models.Entities;
using System;
using System.Threading.Tasks;

namespace Models.Repositories
{
	public interface IUnitOfWork : IDisposable
	{
		IGenericRepository<Category> Category { get; }
		IGenericRepository<Product> Product { get; }
		IGenericRepository<Vendor> Vendor { get; }
		Task SaveAsync();
	}
}
