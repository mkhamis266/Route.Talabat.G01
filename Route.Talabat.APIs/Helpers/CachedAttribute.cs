using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Core.Services.Contract;
using System.Text;

namespace Route.Talabat.APIs.Helpers
{
	public class CashedAttribute : Attribute, IAsyncActionFilter
	{
		private readonly int _timeToLiveSeconds;

		public CashedAttribute(int timeToLiveSeconds)
		{
			_timeToLiveSeconds = timeToLiveSeconds;
		}
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var cachingService = context.HttpContext.RequestServices.GetRequiredService<ICachingService>();

			var cachKey = GenerateCashKeyFromRequest(context.HttpContext.Request);

			var getResponseCaching = await cachingService.GetCashedResponseAsync(cachKey);
			if (string.IsNullOrEmpty(getResponseCaching))
			{
				var result = new ContentResult()
				{
					Content = getResponseCaching,
					ContentType = "application/json",
					StatusCode = 200
				};
				context.Result = result;
				return;
			}

			var nextExecution = await next.Invoke();
			if (nextExecution.Result is OkObjectResult okObjectResult && okObjectResult.Value != null)
			{
				await cachingService.CacheResponseAsync(cachKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveSeconds));
			}
		}

		private string GenerateCashKeyFromRequest(HttpRequest request)
		{
			//{{baseUrl}}/api/product?pageIndex=1&pageSize=5&sort=name
			var keyBuilder = new StringBuilder();
			keyBuilder.Append(request.Path);

			foreach (var (key, value) in request.Query)
			{
				keyBuilder.Append($"|{key}-{value}");
			}
			return keyBuilder.ToString();
		}
	}
}
