using IntegrationApi.Data.SMSServices;
using IntegrationModels;
using LoginModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [Route("api/[controller]")]
    [ApiController]
	public class SMSServiceController : ControllerBase
	{
		private readonly ISMSProvider sMSProvider;

		public SMSServiceController(ISMSProvider sMSProvider)
		{
			this.sMSProvider = sMSProvider;
		}
        [HttpPost]
        public MessageEF Main(SMS sMS)
		{
			return sMSProvider.Main(sMS);
		}
        [HttpPost]
        public SMSResponseData TestSMS(string MobileNo)
		{
			return sMSProvider.TestSMS(MobileNo);
		}
	}
}
