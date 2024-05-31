using Microsoft.AspNetCore.Builder;

namespace Route.Talabat.APIs.Extensions
{
	public static class SwaggerServicesExtension
	{
		public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
		{
			// Add all services of swagger to DI container
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();

			return services;
		}

		public static WebApplication UseSwaggerMiddlewares(this WebApplication app)
		{
			app.UseSwagger();
			app.UseSwaggerUI();

			return app;
		}
	}
}
