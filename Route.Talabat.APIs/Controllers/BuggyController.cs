using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route.Talabat.APIs.Errors;
using Route.Talabat.Infrastructure.Data;

namespace Route.Talabat.APIs.Controllers
{
	public class BuggyController : BaseApiController
	{
		private readonly ApplicationDbContext _dbcontext;

		public BuggyController(ApplicationDbContext dbcontext)
        {
			_dbcontext = dbcontext;
		}

		[HttpGet("notfound")]
		public ActionResult GetNotFoundRequest()
		{
			var Product = _dbcontext.Products.Find(100);
			if(Product is not null)
				return Ok(Product);

			return NotFound(new ApiResponse(404));
		}

		[HttpGet("servererror")]
		public ActionResult GetServerError()
		{
			var Product = _dbcontext.Products.Find(100);
			var ProductToReturn = Product.ToString();
			return Ok(ProductToReturn);
		}
		[HttpGet("badrequest")]
		public ActionResult GetBaddRequest()
		{
			return BadRequest(new ApiResponse(400));
		}
		[HttpGet("badrequest/{id}")] //badrequest/five
		public ActionResult GetBaddRequest(int id)
		{
			return Ok();
		}
	}
}
