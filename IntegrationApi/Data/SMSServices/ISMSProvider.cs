using IntegrationModels;
using LoginModels;

namespace IntegrationApi.Data.SMSServices
{
	public interface ISMSProvider
	{
		MessageEF Main(SMS sMS);
		SMSResponseData TestSMS(string MobileNo);
	}
}
