using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Errors;
using Route.Talabat.APIs.Helpers;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Infrastructure;
using Route.Talabat.Core.Redis;
using Route.Talabat.Core;
using Route.Talabat.Core.Services.Contract;
using Route.Talabat.Services.OrderService;
using Route.Talabat.Services.ProductService;
using Route.Talabat.Services.PaymentService;

namespace Route.Talabat.APIs.Extensions
{
	public static class ApplicationServicesExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
			services.AddScoped(typeof(IOrderService), typeof(OrderService));
			//services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped(typeof(IProductService), typeof(ProductService));	
			services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));

			services.AddAutoMapper(typeof(MappingProfiles));
			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count > 0)
														.SelectMany(P => P.Value.Errors)
														.Select(E => E.ErrorMessage)
														.ToArray();
					var resonse = new ApiValidationErrorResponse()
					{
						Errors = errors
					};

					return new BadRequestObjectResult(resonse);
				};
			});
			services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
			return services;
		}
	}
}
