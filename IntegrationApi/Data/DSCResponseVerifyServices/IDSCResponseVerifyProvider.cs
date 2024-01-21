using IntegrationModels;
using LoginModels;

namespace IntegrationApi.Data.DSCResponseVerifyServices
{
	public interface IDSCResponseVerifyProvider
	{
		Task<MessageEF> InsertDSCRespnseData(DSCResponse objDSCResponseModel);
	}
}
