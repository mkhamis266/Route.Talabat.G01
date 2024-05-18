using AutoMapper;
using Route.Talabat.APIs.DTOs;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Entities.Basket;
using Route.Talabat.Core.Entities.Identity;
using Route.Talabat.Core.Entities.Order_Aggregate;

namespace Route.Talabat.APIs.Helpers
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Product, ProductToReturnDTO>()
				.ForMember(P => P.Brand, O => O.MapFrom(S => S.Brand.Name))
				.ForMember(P => P.Category, O => O.MapFrom(S => S.Category.Name))
				.ForMember(P => P.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());
			CreateMap<CustomerBasketDTO, CustomerBasket>();
			CreateMap<BasketItemDTO, BasketItem>();
			CreateMap<Address, AddressDTO>().ReverseMap();
			CreateMap<ShippingAddressDTO, ShippingAddress>();

			CreateMap<OrderItem, OrderItemDTO>()
				.ForMember(orderItemDto => orderItemDto.ProductId, O => O.MapFrom(orderItem => orderItem.Product.ProductId))
				.ForMember(orderItemDto => orderItemDto.ProductName, O => O.MapFrom(orderItem => orderItem.Product.ProductName))
				.ForMember(orderItemDto => orderItemDto.PictureURL, O => O.MapFrom(orderItem => orderItem.Product.PictureURL))
				.ForMember(orderItemDto => orderItemDto.PictureURL, O =>
				{
					O.MapFrom<OrderItemPictureUrlResolver>();
				});

			CreateMap<Order, OrderToReturnDTO>()
				.ForMember(ordrToReturnDto => ordrToReturnDto.DeliveyMethod, O => O.MapFrom(order => order.DelivreyMethod.ShortName))
				.ForMember(ordrToReturnDto => ordrToReturnDto.DeliveyMethodCoast, O => O.MapFrom(order => order.DelivreyMethod.Cost));


		}
	}
}
