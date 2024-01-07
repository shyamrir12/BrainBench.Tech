using IntegrationApi.Data.SMSServices;
using IntegrationModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SMSServiceController : ControllerBase
	{
		private readonly ISMSProvider sMSProvider;

		public SMSServiceController(ISMSProvider sMSProvider)
		{
			this.sMSProvider = sMSProvider;
		}

		public MessageEF Main(SMS sMS)
		{
			return sMSProvider.Main(sMS);
		}

		public SMSResponseData TestSMS(string MobileNo)
		{
			return sMSProvider.TestSMS(MobileNo);
		}
	}
}
