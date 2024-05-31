using System.Net;
using System.Text.Json;
using Route.Talabat.APIs.Errors;

namespace Route.Talabat.APIs.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;
		private readonly IHostEnvironment _env;

		public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,IHostEnvironment env)
		{
			_next = next;
			_logger = logger;
			_env = env;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				// Take Action with Request
				 await _next.Invoke(httpContext); // invoke the next middleware
				// Take Action with Response
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message); // dev env
				// log to database | file     // prod env

				httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
				httpContext.Response.ContentType = "application/json";

				var response = _env.IsDevelopment() ?
					new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
					:
					new ApiExceptionResponse((int) HttpStatusCode.InternalServerError);

				var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
				var json = JsonSerializer.Serialize(response, options);
				await httpContext.Response.WriteAsync(json);
			}

		}
	}
}
