using LoginModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		private readonly ILogger<ValuesController> _logger;
		public ValuesController(ILogger<ValuesController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public ActionResult<Result<string[]>> Get()
		{
			Result<string[]> res = new Result<string[]>();
			
			try
			{
				//int a, b, c;
				//a = 10;b = 0;
				//c = a / b;
				res.Data = new string[] { "value1", "value2" };
				res.Message = new System.Collections.Generic.List<string>() { "Success" };
				res.Status = true;
			}
			catch (Exception ex) {

				res.Data = null;
				res.Message = new System.Collections.Generic.List<string>() { "Exception : " + ex.Message };
				res.Status = false;
				_logger.LogError(ex, ex.Message);
			}

			return res;

			
		}
	}
}
