using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Entities.Order_Aggregate;
using Route.Talabat.Core.Repositories.Contract;

namespace Route.Talabat.Core
{
	public interface IUnitOfWork:IAsyncDisposable
	{

		public IGenericRepository<T> Repository<T>() where T : BaseEntity;

		public Task<int> Compelete();
    }
}
