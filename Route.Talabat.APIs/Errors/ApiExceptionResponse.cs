namespace Route.Talabat.APIs.Errors
{
	public class ApiExceptionResponse : ApiResponse
	{
        public string? Detailes { get; set; }
        public ApiExceptionResponse(int statusCode, string? message = null, string? details = null) : base(statusCode, message)
		{
			Detailes = details;
		}
	}
}
