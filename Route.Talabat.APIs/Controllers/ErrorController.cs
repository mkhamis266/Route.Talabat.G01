using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Errors;

namespace Route.Talabat.APIs.Controllers
{
	[Route("errors/{code}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorController : ControllerBase
	{
		public ActionResult Error(int code)
		{
			if (code == 400)
				return BadRequest(new ApiResponse(code));
			else if (code == 401)
				return Unauthorized(new ApiResponse(code));
			else if (code == 404)
				return NotFound(new ApiResponse(code));
			else
				return StatusCode(code);
		}
	}
}
