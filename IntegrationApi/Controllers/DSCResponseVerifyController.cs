using IntegrationApi.Data.DSCResponseVerifyServices;
using IntegrationModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationApi.Controllers
{
	[Route("api/{controller}/{action}")]
	[ApiController]
	public class DSCResponseVerifyController : ControllerBase
	{
		private readonly IDSCResponseVerifyProvider _objIDSCResponseVerifyProvider;

		public DSCResponseVerifyController(IDSCResponseVerifyProvider objIDSCResponseVerifyProvider)
		{
			_objIDSCResponseVerifyProvider = objIDSCResponseVerifyProvider;
		}

		[HttpPost]
		public async Task<MessageEF> InsertDSCRespnseData(DSCResponse objDSCResponseModel)
		{
			return await _objIDSCResponseVerifyProvider.InsertDSCRespnseData(objDSCResponseModel);
		}
	}
}
