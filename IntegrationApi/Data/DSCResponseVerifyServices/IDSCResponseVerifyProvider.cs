using IntegrationModels;

namespace IntegrationApi.Data.DSCResponseVerifyServices
{
	public interface IDSCResponseVerifyProvider
	{
		Task<MessageEF> InsertDSCRespnseData(DSCResponse objDSCResponseModel);
	}
}
