using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Models.Repositories
{
	public interface IGenericRepository<T> where T : class
	{
		
		Task<T> AddAsync(T entity);
		Task<T> GetByIdAsync(int id);
		Task<IEnumerable<T>> GetAllAsync();		
		Task<T> UpdateAsync(T entity);
		Task DeleteAsync(int id);		
		Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate);
		Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
	}
}
