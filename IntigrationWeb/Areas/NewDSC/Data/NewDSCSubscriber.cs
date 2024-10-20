using LoginModels;
using IntegrationModels;
using Newtonsoft.Json;
using IntigrationWeb.Models.Utility;

namespace IntigrationWeb.Areas.NewDSC.Data
{
    public class NewDSCSubscriber : INewDSCSubscriber
    {
        IHttpWebClients _objIHttpWebClients;
        public NewDSCSubscriber(IHttpWebClients objIHttpWebClients)
        {
            _objIHttpWebClients = objIHttpWebClients;
        }
        public MessageEF SaveDSCFilePath(NewDSCRequest objParams)
        {
            try
            {
                return JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("NewDSC/SaveDSCFilePath", JsonConvert.SerializeObject(objParams)));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
