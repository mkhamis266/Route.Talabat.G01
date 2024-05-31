using AutoMapper;
using Route.Talabat.APIs.DTOs;
using Route.Talabat.Core.Entities;

namespace Route.Talabat.APIs.Helpers
{
	public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
	{
		private readonly IConfiguration _configs;

		public ProductPictureUrlResolver(IConfiguration configs)
        {
			_configs = configs;
		}
        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
		{
			if (!String.IsNullOrEmpty(source.PictureUrl))
				return $"{_configs["ApiBaseUrl"]}/{source.PictureUrl}";
			
			return String.Empty;
		}
	}
}
