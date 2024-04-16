using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Route.Talabat.APIs.Helpers;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Infrastructure;
using Route.Talabat.Infrastructure.Data;

namespace Route.Talabat.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var webApplicationBuilder = WebApplication.CreateBuilder(args);

			#region ConfigureServices
			// Add services to the DI container.
			webApplicationBuilder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

			// Add all services of swagger to DI container
			webApplicationBuilder.Services.AddEndpointsApiExplorer();
			webApplicationBuilder.Services.AddSwaggerGen();

			webApplicationBuilder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));

			webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>((options) =>
			{
				options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("defaultConnection"));
			});

			webApplicationBuilder.Services.AddAutoMapper(typeof(MappingProfiles));
			#endregion

			var app = webApplicationBuilder.Build();

			using var Scope = app.Services.CreateScope();
			var Services = Scope.ServiceProvider;
			var loggerFactory = Services.GetRequiredService<ILoggerFactory>();
			var _dbContext = Services.GetRequiredService<ApplicationDbContext>();
			//Ask CLR for object form ApplicationDbContextExplicitly
			try
			{
				await _dbContext.Database.MigrateAsync(); // Apply All Migration 
				await ApplicationContextSeed.SeedData(_dbContext); //Data Seeding
			}
			catch (Exception ex)
			{
				var Logger = loggerFactory.CreateLogger("Logger");
				Logger.LogWarning(ex, "An Error Occured wile updating database");
			}


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
