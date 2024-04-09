  namespace Route.Talabat.APIs
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var webApplicationBuilder = WebApplication.CreateBuilder(args);

			#region ConfigureServices
			// Add services to the DI container.
			webApplicationBuilder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			
			// Add all services of swagger to DI container
			webApplicationBuilder.Services.AddEndpointsApiExplorer();
			webApplicationBuilder.Services.AddSwaggerGen(); 
			#endregion

			var app = webApplicationBuilder.Build();

			#region Configure
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			//app.UseRouting();
			//app.UseEndpoints(endpoints =>
			//{
			//  /// for mvc
			//	//endpoints.MapControllerRoute(
			//	//	name: "default",
			//	//	pattern: "api/{controller}/{action}/{id?}"
			//	//	);

			// /// for api
			//	endpoints.MapControllers();
			//});

			// use this middelware instead of the 2 commented in top
			app.MapControllers();
			#endregion

			app.Run();
		}
	}
}
