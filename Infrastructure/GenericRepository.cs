using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Infrastructure.Data;

namespace Route.Talabat.Infrastructure
{
	public class GenericRepository<T> : IGenericRepository<T>  where T : BaseEntity
	{
		private readonly ApplicationDbContext _dbContext;

		public GenericRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<IEnumerable<T>> GetAllAsync()
		{
			if(typeof(T) == typeof(Product))
				return (IEnumerable < T >) await _dbContext.Products.Include(P => P.Brand).Include
					(P => P.Category).ToListAsync();
			return await _dbContext.Set<T>().ToListAsync();
		}

		public async Task<T?> GetAsync(int id)
		{
			if (typeof(T) == typeof(Product))
				return await _dbContext.Set<Product>().Where(P => P.Id == id).Include(P => P.Brand).Include
					(P => P.Category).FirstOrDefaultAsync() as T;

				return await _dbContext.FindAsync<T>(id);
		}
	}
}
