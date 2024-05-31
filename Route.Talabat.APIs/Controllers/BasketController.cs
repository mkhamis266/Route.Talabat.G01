using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.DTOs;
using Route.Talabat.APIs.Errors;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Entities.Basket;
using Route.Talabat.Core.Repositories.Contract;

namespace Route.Talabat.APIs.Controllers
{
	
	public class BasketController : BaseApiController
	{
		private readonly IBasketRepository _basketRepo;
		private readonly IMapper _mapper;

		public BasketController(IBasketRepository basketRepo,IMapper mapper)
        {
			_basketRepo = basketRepo;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string id) 
		{
			var basket = await _basketRepo.GetCustomerBasket(id);
			if (basket is null)
				return NotFound(new ApiResponse(404));
			return Ok(basket);
		}

		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> UpdateCustomerBasket(CustomerBasketDTO basket)
		{
			var mappedBasket = _mapper.Map<CustomerBasketDTO,CustomerBasket>(basket);
			var returnedBasket = await _basketRepo.UpdateCustomerBasket(mappedBasket);
			if (returnedBasket is null) return BadRequest(new ApiResponse(400));
			return Ok(returnedBasket);
		}

		[HttpDelete]
		public async Task DeleteCustomerBasket(string id)
		{
			await _basketRepo.DeleteCustomerBasket(id);	
		}
    }
}
