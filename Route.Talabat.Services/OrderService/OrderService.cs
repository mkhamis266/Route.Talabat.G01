using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Entities.Order_Aggregate;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Core.Services.Contract;
using Route.Talabat.Infrastructure;
using Route.Talabat.Infrastructure.Data;

namespace Route.Talabat.Services.OrderService
{
	public class OrderService : IOrderService
	{
		private readonly IGenericRepository<Product> _productsRepo;
		private readonly IBasketRepository _basketRepo;
		private readonly IGenericRepository<DelivreyMethod> _deliveryMethodsRepo;
		private readonly IGenericRepository<Order> _orderRepo;

		public OrderService(
			IGenericRepository<Product> productsRepo, 
			IBasketRepository basketRepo,
			IGenericRepository<DelivreyMethod> deliveryMethodsRepo,
			IGenericRepository<Order> orderRepo
			)
        {
			_productsRepo = productsRepo;
			_basketRepo = basketRepo;
			_deliveryMethodsRepo = deliveryMethodsRepo;
			_orderRepo = orderRepo;
		}
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
		{
			//1- get basket form basket repo
			var basket = await _basketRepo.GetCustomerBasket(basketId);
            //2- get selected items at basket form products repo 
			List<OrderItem> orderItems = new List<OrderItem>();

			if (basket?.Items?.Count > 0)
			{
				foreach (var item in basket.Items)
				{
					var product = await _productsRepo.GetAsync(item.Id);
					var productItemOrdered = new ProductItemOrdered(item.Id, product?.Name?? String.Empty,product?.PictureUrl?? String.Empty);

					var orderItem = new OrderItem(productItemOrdered, item.Quantity, product.Price);
					orderItems.Add(orderItem);
				}
			}
			//3- claculate sub-total
			decimal subTotal = orderItems.Sum(item=> item.Quantity*item.Price);

			//4- get deliveryMethod from deliveryMethods repo
			var deliveryMethod = await _deliveryMethodsRepo.GetAsync(deliveryMethodId);
			//5- create order
			Order order = new Order() {
				BuyerEmail = buyerEmail,
				DeliveyMethod = deliveryMethod,
				ShippingAddress = shippingAddress,
				SubTotal = subTotal,
				Items = orderItems,
				};
			//6- save To Database

			_orderRepo.Add(order);
			return order;
        }

		public Task<IReadOnlyList<DelivreyMethod>> GetDelivreyMethodsAsync()
		{
			throw new NotImplementedException();
		}

		public Task<Order> GetOrderByIdAsync(string buyerEmail, int orderId)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
		{
			throw new NotImplementedException();
		}
	}
}
