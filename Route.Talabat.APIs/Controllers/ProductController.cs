using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Repositories.Contract;

namespace Route.Talabat.APIs.Controllers
{
	public class ProductController : BaseApiController
	{
		private readonly IGenericRepository<Product> _genericRepository;

		public ProductController(IGenericRepository<Product> genericRepository)
		{
			_genericRepository = genericRepository;
		}
	}
}
