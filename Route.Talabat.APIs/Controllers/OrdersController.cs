using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.DTOs;
using Route.Talabat.APIs.Errors;
using Route.Talabat.Core.Entities.Order_Aggregate;
using Route.Talabat.Core.Services.Contract;

namespace Route.Talabat.APIs.Controllers
{
	public class OrdersController : BaseApiController
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrdersController(IOrderService orderService, IMapper mapper)
		{
			_orderService = orderService;
			_mapper = mapper;
		}

		[ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
		[HttpPost]
		public async Task<ActionResult<Order?>> CreateOrder (OrderDTO model)
		{
			var address = _mapper.Map<ShippingAddressDTO,ShippingAddress>(model.ShippingAddress);
			var order = await _orderService.CreateOrderAsync(model.BuyerEmail, model.BasketId,model.DeliveryMethodId,address );
			if (order is null) return BadRequest(new ApiResponse(400));
			return Ok(order);
		}
	}
}
