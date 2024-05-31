using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Errors;
using Route.Talabat.Core.Entities.Basket;
using Route.Talabat.Core.Entities.Order_Aggregate;
using Route.Talabat.Core.Services.Contract;
using Stripe;

namespace Route.Talabat.APIs.Controllers
{
	public class PaymentController : BaseApiController
	{
		private readonly IPaymentService _paymentService;
		private readonly ILogger<PaymentController> _logger;
		private readonly string endpointSecret = "whsec_33d79d429a78fb428e173d5c93f4ee154f7bbb47008984e479c1d42038e8eedf";

		public PaymentController(IPaymentService paymentService,ILogger<PaymentController> logger)
		{
			_paymentService = paymentService;
			_logger = logger;
		}

		[HttpPost("{basketid}")]
		[Authorize]
		[ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
		{
			var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
			if (basket is null) return BadRequest(new ApiResponse(400,"basket not found"));
			return Ok(basket);
		}

		[HttpPost("webhook")]
		public async Task<IActionResult> webhook()
		{
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
			var stripeEvent = EventUtility.ConstructEvent(json,
								Request.Headers["Stripe-Signature"], endpointSecret);

			Order? order;
			var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
			// Handle the event
			if (stripeEvent.Type == Events.PaymentIntentSucceeded)
			{
				order = await _paymentService.UpdateOrderStatus(paymentIntent.Id, true);
				_logger.LogInformation("Order is Succeded: {0}", order?.PaymentIntentId);
			}
			else if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
			{
				order = await _paymentService.UpdateOrderStatus(paymentIntent.Id, false);
				_logger.LogInformation("Order is Failed: {0}", order?.PaymentIntentId);

			}


			return Ok();
		}
	}
}
