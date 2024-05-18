using Route.Talabat.Core;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Entities.Order_Aggregate;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Core.Services.Contract;
using Route.Talabat.Core.specifications.OrderSpecs;
using Stripe;
using Product = Route.Talabat.Core.Entities.Product;

namespace Route.Talabat.Services.OrderService
{
	public class OrderService : IOrderService
	{
		private readonly IBasketRepository _basketRepo;
		///private readonly IGenericRepository<Product> _productsRepo;
		///private readonly IGenericRepository<DelivreyMethod> _deliveryMethodsRepo;
		///private readonly IGenericRepository<Order> _orderRepo;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPaymentService _paymentService;

		public OrderService(
			IBasketRepository basketRepo,
			IUnitOfWork unitOfWork,
			IPaymentService paymentService
			///IGenericRepository<Product> productsRepo, 
			///IGenericRepository<DelivreyMethod> deliveryMethodsRepo,
			///IGenericRepository<Order> orderRepo
			)
        {
			_basketRepo = basketRepo;
			_unitOfWork = unitOfWork;
			_paymentService = paymentService;
			///_productsRepo = productsRepo;
			///_deliveryMethodsRepo = deliveryMethodsRepo;
			///_orderRepo = orderRepo;
		}
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, ShippingAddress shippingAddress)
		{
			//1- get basket form basket repo
			var basket = await _basketRepo.GetCustomerBasket(basketId);
            //2- get selected items at basket form products repo 
			List<OrderItem> orderItems = new List<OrderItem>();

			if (basket?.Items?.Count > 0)
			{
				foreach (var item in basket.Items)
				{
					var product = await _unitOfWork.Repository<Product>().GetAsync(item.Id);
					var productItemOrdered = new ProductItemOrdered(item.Id, product?.Name?? String.Empty,product?.PictureUrl?? String.Empty);

					var orderItem = new OrderItem(productItemOrdered, item.Quantity, product.Price);
					orderItems.Add(orderItem);
				}
			}
			//3- claculate sub-total
			decimal subTotal = orderItems.Sum(item=> item.Quantity*item.Price);

			//4- get deliveryMethod from deliveryMethods repo
			var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);

			var orderSpec = new OrderWithPaymenyIntentIdSpecs(basket.PaymenyIntentId);
			var orderRepo = _unitOfWork.Repository<Order>();
			var exsistingOrder = await orderRepo.GetWithSpecAsync(orderSpec);

			if (exsistingOrder is not null)
			{
				orderRepo.Delete(exsistingOrder);
				await _paymentService.CreateOrUpdatePaymentIntent(basket.Id);
			}
			//5- create order
			Order order = new Order(buyerEmail: buyerEmail,shippingAddress: shippingAddress,deliveryMethod:deliveryMethod, items:orderItems,subTotal:subTotal,paymentIntentId:basket.PaymenyIntentId);
			orderRepo.Add(order);

			//6- save To Database
			var result = await _unitOfWork.Compelete();

			if (result <= 0) return null;

			return order;
        }

		public async Task<IReadOnlyList<DeliveryMethod>> GetDelivreyMethodsAsync()
			=> await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();

		public async Task<Order?> GetOrderByIdForUserAsync(string buyerEmail, int orderId)
		{
			var ordersRepos = _unitOfWork.Repository<Order>();
			var orderSpecifications = new OrderSpecifications(buyerEmail,orderId);
			var order = await ordersRepos.GetWithSpecAsync(orderSpecifications);
			return order;
		}

		public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
		{
			var ordersRepos = _unitOfWork.Repository<Order>();
			var orderSpecifications = new OrderSpecifications(buyerEmail);
			var orders = ordersRepos.GetAllWithSpecAsync(orderSpecifications);
			return orders;
		}
	}
}
