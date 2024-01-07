using IntegrationApi.Data.EncryptionServices;
using IntegrationModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationApi.Controllers
{
	[Route("api/{controller}/{action}")]
	[ApiController]
	public class EncryptionServiceController : ControllerBase
	{
		private readonly IEncryptionProvider _iEncryptionService;
		public EncryptionServiceController(IEncryptionProvider iEncryptionService)
		{
			this._iEncryptionService = iEncryptionService;
		}

		[HttpPost]
		public EncryptionModel GetDecryptVal(EncryptionModel encout)
		{
			return _iEncryptionService.GetDecryptVal(encout);
		}

		[HttpPost]
		public EncryptionModel GetEncryptVal(EncryptionModel encout)
		{
			return _iEncryptionService.GetEncryptVal(encout);
		}
	}
}
