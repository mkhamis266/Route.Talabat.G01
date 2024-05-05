using AutoMapper;
using Route.Talabat.APIs.DTOs;
using Route.Talabat.Core.Entities.Order_Aggregate;

namespace Route.Talabat.APIs.Helpers
{
	public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDTO, string>
	{
		private readonly IConfiguration _config;

		public OrderItemPictureUrlResolver(IConfiguration config)
		{
			_config = config;
		}

		public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
		{
			if (!String.IsNullOrEmpty(source.Product.PictureURL))
				return $"{_config["ApiBaseUrl"]}/{source.Product.PictureURL}";

			return String.Empty;
		}
	}
}
