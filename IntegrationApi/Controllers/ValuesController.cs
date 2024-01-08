using IntegrationApi.Data.ExceptionDataServices;
using LoginModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		private readonly IExceptionDataProvider _objIExceptionProvider;
		public ValuesController(IExceptionDataProvider objIExceptionProvider)
		{
			_objIExceptionProvider = objIExceptionProvider;
		}

		[HttpGet]
		public ActionResult<Result<string[]>> Get()
		{
			Result<string[]> res = new Result<string[]>();
			
			try
			{
				//int a, b, c;
				//a = 10; b = 0;
				//c = a / b;
				res.Data = new string[] { "value1", "value2" };
				res.Message = new List<string>() { "Success" };
				res.Status = true;
			}
			catch (Exception ex) {

				res.Data =null;
				res.Message = new List<string>() { "fail",ex.Message };
				res.Status = false;
				_objIExceptionProvider.ErrorLoger(ex, HttpContext);


			}
			

			return res;

			
		}
	}
}
