using IntegrationModels;
using LoginModels;

namespace IntigrationWeb.Areas.OldDSC.Data
{
    public interface IDSCResponseVerifySubscriber
    {
        MessageEF InsertDSCRespnseData(DSCResponse objDSCResponseModel);
    }
}
