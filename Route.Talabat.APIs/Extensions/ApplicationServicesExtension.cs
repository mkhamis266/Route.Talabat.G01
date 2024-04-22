using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Errors;
using Route.Talabat.APIs.Helpers;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Infrastructure.Data;
using Route.Talabat.Infrastructure;
using StackExchange.Redis;

namespace Route.Talabat.APIs.Extensions
{
	public static class ApplicationServicesExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

			

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
			
			return services;
		}
	}
}
