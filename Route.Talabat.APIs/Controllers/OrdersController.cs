using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.DTOs;
using Route.Talabat.APIs.Errors;
using Route.Talabat.Core.Entities.Order_Aggregate;
using Route.Talabat.Core.Services.Contract;
using StackExchange.Redis;

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

		[ProducesResponseType(typeof(OrderToReturnDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
		[HttpPost]
		[Authorize]
		public async Task<ActionResult<OrderToReturnDTO?>> CreateOrder(OrderDTO model)
		{
			var address = _mapper.Map<ShippingAddressDTO, ShippingAddress>(model.ShippingAddress);
			var order = await _orderService.CreateOrderAsync(model.BuyerEmail, model.BasketId, model.DeliveryMethodId, address);
			if (order is null) return BadRequest(new ApiResponse(400));
			var orderToReturnDto = _mapper.Map<Core.Entities.Order_Aggregate.Order, OrderToReturnDTO>(order);
			return Ok(orderToReturnDto);
		}

		[HttpGet] // GET : /api/Orders?email=""
		public async Task<ActionResult<IReadOnlyList<OrderToReturnDTO>>> GetOrdersForUser()
		{
			var email = User.FindFirstValue(ClaimTypes.Email) ?? String.Empty;
			var orders = await _orderService.GetOrdersForUserAsync(email);
			var orderToReturnDto = _mapper.Map<IReadOnlyList<Core.Entities.Order_Aggregate.Order>, IReadOnlyList<OrderToReturnDTO>>(orders);
			return Ok(orderToReturnDto);
		}

		[ProducesResponseType(typeof(OrderToReturnDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[HttpGet("{id}")]
		[Authorize]
		public async Task<ActionResult<OrderToReturnDTO>> GetOrderForUser(int id)
		{
			var email = User.FindFirstValue(ClaimTypes.Email) ?? String.Empty;
			var order = await _orderService.GetOrderByIdForUserAsync(email, id);
			if (order is null) return NotFound(new ApiResponse(404));
			var orderToReturnDto = _mapper.Map<Core.Entities.Order_Aggregate.Order, OrderToReturnDTO>(order);
			return Ok(orderToReturnDto);
		}

		[HttpGet("deliverymethods")]
		public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
		{
			var deliveryMethod = await _orderService.GetDelivreyMethodsAsync();
			return Ok(deliveryMethod);
		}
	}
}
