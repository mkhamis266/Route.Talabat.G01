using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.specifications;

namespace Route.Talabat.Core.Repositories.Contract
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<T?> GetAsync(int id);

		Task<IReadOnlyList<T>> GetAllAsync();

		Task<T?> GetWithSpecAsync(ISpecifications<T> specs);
		Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> specs);

		Task<int> GetCountAsync(ISpecifications<T> specs);
	}
}
