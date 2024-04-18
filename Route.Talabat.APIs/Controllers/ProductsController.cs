using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.DTOs;
using Route.Talabat.APIs.Errors;
using Route.Talabat.APIs.Helpers;
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
		private readonly IGenericRepository<ProductCategory> _productCatigoriesRepo;
		private readonly IGenericRepository<ProductBrand> _productsBrandsRepo;

		public ProductsController(IGenericRepository<Product> genericRepository,IMapper mapper,
			IGenericRepository<ProductCategory> productCatigoriesRepo,
			IGenericRepository<ProductBrand> productsBrandsRepo
			)
		{
			productsRepository = genericRepository;
			_mapper = mapper;
			_productCatigoriesRepo = productCatigoriesRepo;
			_productsBrandsRepo = productsBrandsRepo;
		}

		[HttpGet]
		public async Task<ActionResult<Pagination<ProductToReturnDTO>>> GetAllProducts([FromQuery]ProductSpecificationsParams productParams)
		{
			//var Products = await productsRepository.GetAllAsync();
			var productSpecs = new ProductsWithBrandAndCategorySpecifications(productParams);
			var Products = await productsRepository.GetAllWithSpecAsync(productSpecs);
			var Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(Products);
			var productSpecsForCount = new ProductWithFiltersForCountSpecifications(productParams); 
			var Count = await productsRepository.GetCountAsync(productSpecsForCount);
			return Ok(new Pagination<ProductToReturnDTO>(productParams.PageIndex, productParams.PageSize,Count,Data));
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(ProductToReturnDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ProductToReturnDTO>> GetProductById(int id)
		{
			//var Product = await productsRepository.GetAsync(id);
			var ProductSpecs = new ProductsWithBrandAndCategorySpecifications(id);
			var Product = await productsRepository.GetWithSpecAsync(ProductSpecs);
			if(Product is null)
				return NotFound(new { Message = "Not Found",StatusCode = 404});
			return Ok(_mapper.Map<Product,ProductToReturnDTO>(Product));
		}

		[HttpGet("brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
		{
			var brands =  await _productsBrandsRepo.GetAllAsync();
			return Ok(brands);
		}

		[HttpGet("categories")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductCategories()
		{
			var categories = await _productCatigoriesRepo.GetAllAsync();
			return Ok(categories);
		}
	}
}
