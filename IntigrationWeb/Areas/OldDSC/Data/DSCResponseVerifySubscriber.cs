using IntegrationModels;
using IntigrationWeb.Models.Utility;
using LoginModels;
using Newtonsoft.Json;

namespace IntigrationWeb.Areas.OldDSC.Data
{
    public class DSCResponseVerifySubscriber : IDSCResponseVerifySubscriber
    {
       private readonly IHttpWebClients _objIHttpWebClients;
        public DSCResponseVerifySubscriber(IHttpWebClients objIHttpWebClients)
        {

            _objIHttpWebClients = objIHttpWebClients;
        }
        public MessageEF InsertDSCRespnseData(DSCResponse objDSCResponseModel)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("DSCResponseVerify/InsertDSCRespnseData", JsonConvert.SerializeObject(objDSCResponseModel)));
                return objMessageEF;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
