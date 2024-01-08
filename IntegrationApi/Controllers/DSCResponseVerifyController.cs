using IntegrationApi.Data.DSCResponseVerifyServices;
using IntegrationApi.Data.ExceptionDataServices;
using IntegrationModels;
using LoginModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationApi.Controllers
{
	[Route("api/{controller}/{action}")]
	[ApiController]
	public class DSCResponseVerifyController : ControllerBase
	{
		private readonly IDSCResponseVerifyProvider _objIDSCResponseVerifyProvider;
		private readonly IExceptionDataProvider _objIExceptionProvider;
		public DSCResponseVerifyController(IDSCResponseVerifyProvider objIDSCResponseVerifyProvider, IExceptionDataProvider objIExceptionProvider)
		{
			_objIDSCResponseVerifyProvider = objIDSCResponseVerifyProvider;
			_objIExceptionProvider = objIExceptionProvider;
		}

		[HttpPost]
		public async Task<Result<MessageEF>> InsertDSCRespnseData(DSCResponse objDSCResponseModel)
		{

			Result<MessageEF> res=new Result<MessageEF> ();
			try
			{
				res.Data = await _objIDSCResponseVerifyProvider.InsertDSCRespnseData(objDSCResponseModel);
				res.Message = new List<string>() { "Success" };
				res.Status = true;
			
			}
			catch (Exception ex) {

				res.Data = null;
				res.Message = new List<string>() { "Fail", ex.Message };
				res.Status = false;
				_objIExceptionProvider.ErrorLoger(ex, HttpContext); 
			
			}
			
			return res;
		}
	}
}
