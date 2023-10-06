using Data.DataContext;
using Microsoft.EntityFrameworkCore;
using Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Sevices
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		protected readonly AppDbContext _context;

		public GenericRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<T> AddAsync(T entity)
		{
			 _context.Set<T>().Add(entity);
			_context.SaveChanges();
			return entity;			
		}

		public async Task<T> GetByIdAsync(int id)
		{
			var result = await _context.Set<T>().FindAsync(id);
			if (result == null)
			{
				return null;
			}
			return result;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			var results = await _context.Set<T>().ToListAsync();
			return results;
		}

		public async Task DeleteAsync(int id)
		{
			var result = await _context.Set<T>().FindAsync(id);
			if (result == null)
			{
				return;	
			}
			_context.Set<T>().Remove(result);
			await _context.SaveChangesAsync();
			return;
		}

		public async Task<T> UpdateAsync(T entity)
		{
			var result = await _context.Set<T>().FindAsync(entity);
			_context.Set<T>().Update(result);
			await _context.SaveChangesAsync();
			return entity;
		}
		public async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate)
		{
			return await _context.Set<T>().Where(predicate).ToListAsync();
		}

		public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
		{
			return await _context.Set<T>().AnyAsync(predicate);
		}
	}
}
