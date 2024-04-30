using AutoMapper;
using Route.Talabat.APIs.DTOs;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Entities.Identity;

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
		}
	}
}
