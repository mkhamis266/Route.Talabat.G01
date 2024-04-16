using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.DTOs;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Core.specifications;
using Route.Talabat.Core.specifications.ProductSpecs;

namespace Route.Talabat.APIs.Controllers
{
	public class ProductsController : BaseApiController
	{
		private readonly IGenericRepository<Product> productsRepository;
		private readonly IMapper _mapper;

		public ProductsController(IGenericRepository<Product> genericRepository,IMapper mapper)
		{
			productsRepository = genericRepository;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductToReturnDTO>>> GetAllProducts()
		{
			//var Products = await productsRepository.GetAllAsync();
			var productSpecs = new ProductsWithBrandAndCategorySpecifications();
			var Products = await productsRepository.GetAllWithSpecAsync(productSpecs);
			return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDTO>>(Products));
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDTO>> GetProductById(int id)
		{
			//var Product = await productsRepository.GetAsync(id);
			var ProductSpecs = new ProductsWithBrandAndCategorySpecifications(id);
			var Product = await productsRepository.GetWithSpecAsync(ProductSpecs);
			if(Product is null)
				return NotFound(new { Message = "Not Found",StatusCode = 404});
			return Ok(_mapper.Map<Product,ProductToReturnDTO>(Product));
		}
	}
}
