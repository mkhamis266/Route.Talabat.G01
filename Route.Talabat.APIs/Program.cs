using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Route.Talabat.APIs.Errors;
using Route.Talabat.APIs.Extensions;
using Route.Talabat.APIs.Helpers;
using Route.Talabat.APIs.Middlewares;
using Route.Talabat.Core.Entities.Identity;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Core.Services.Contract;
using Route.Talabat.Infrastructure;
using Route.Talabat.Infrastructure.Data;
using Route.Talabat.Infrastructure.Identity;
using Route.Talabat.Services.AuthService;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace Route.Talabat.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var webApplicationBuilder = WebApplication.CreateBuilder(args);

			#region ConfigureServices
			// Add services to the DI container.
			webApplicationBuilder.Services.AddControllers().AddNewtonsoftJson(options => {
				options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
			});
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			webApplicationBuilder.Services.AddSwaggerServices();
			webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>((options) =>
			{
				options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("defaultConnection"));
			});
			webApplicationBuilder.Services.AddApplicationServices();
			webApplicationBuilder.Services.AddSingleton<IConnectionMultiplexer>((servicesProvider) => {
				var connection = webApplicationBuilder.Configuration.GetConnectionString("Redis");
				return ConnectionMultiplexer.Connect(connection);
			});
			webApplicationBuilder.Services.AddDbContext<ApplicationIdentityDbContext>((options) => {
				options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("IdentityConnection"));
			});

			webApplicationBuilder.Services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationIdentityDbContext>();

			webApplicationBuilder.Services.AddScoped<IAuthService, AuthService>();

			webApplicationBuilder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options => 
			{
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateAudience = true,
					ValidAudience = webApplicationBuilder.Configuration["JWT:ValidAudience"],
					ValidateIssuer = true,
					ValidIssuer = webApplicationBuilder.Configuration["JWT:ValidIssuer"],
					ValidateIssuerSigningKey = true,
					IssuerSigningKey =
					new SymmetricSecurityKey(Encoding.UTF8.GetBytes(webApplicationBuilder.Configuration["JWT:AuthKey"] ?? "")),
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero,
				};
			});
			#endregion

			var app = webApplicationBuilder.Build();

			using var Scope = app.Services.CreateScope();
			var Services = Scope.ServiceProvider;
			var loggerFactory = Services.GetRequiredService<ILoggerFactory>();
			var _dbContext = Services.GetRequiredService<ApplicationDbContext>();
			var _applicationIdentityDbContext = Services.GetRequiredService<ApplicationIdentityDbContext>();
			var _userManger = Services.GetRequiredService<UserManager<ApplicationUser>>();
			//Ask CLR for object form ApplicationDbContextExplicitly
			try
			{
				await _dbContext.Database.MigrateAsync(); // Apply All Migration 
				await ApplicationContextSeed.SeedData(_dbContext); //Data Seeding
				await _applicationIdentityDbContext.Database.MigrateAsync();
				await ApplicationIdentityDbContextSeed.DataSeedAsync(_userManger);
			}
			catch (Exception ex)
			{
				var Logger = loggerFactory.CreateLogger("Logger");
				Logger.LogWarning(ex, "An Error Occured wile updating database");
			}


			#region Configure
			// Configure the HTTP request pipeline.

			app.UseMiddleware<ExceptionMiddleware>();
			if (app.Environment.IsDevelopment())
			{
				app.UseSwaggerMiddlewares();
			}

			app.UseStatusCodePagesWithReExecute("/errors/{0}");
			app.UseHttpsRedirection();
			app.UseStaticFiles();
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
