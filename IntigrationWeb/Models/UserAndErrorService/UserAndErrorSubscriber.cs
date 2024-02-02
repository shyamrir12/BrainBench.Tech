using IntegrationModels;
using IntigrationWeb.Models.Utility;
using LoginModels;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace IntigrationWeb.Models.UserAndErrorService
{
    public class UserAndErrorSubscriber : IUserAndErrorSubscriber
    {
        private readonly IHttpWebClients _objIHttpWebClients;

        public UserAndErrorSubscriber(IHttpWebClients objIHttpWebClients)
        {
            _objIHttpWebClients = objIHttpWebClients;
        }

        public string AddExceptionData(LogEntry objLogEntry)
        {

            UserMasterModel userMasterModel = new UserMasterModel();
            try
            {
                var httpMessageReponse = JsonConvert.DeserializeObject<string>(_objIHttpWebClients.PostRequest("PaymentResponses/AddLicenseResponcePayment", JsonConvert.SerializeObject(objLogEntry)));
                return httpMessageReponse;
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

        public  Result<UserLoginSession> GetUserByJWT(AuthenticationResponse model)
        {
            UserMasterModel userMasterModel = new UserMasterModel();
            try
            {
                var httpReponse = JsonConvert.DeserializeObject<Result<UserLoginSession>>(_objIHttpWebClients.PostRequest("PaymentResponses/AddLicenseResponcePayment", JsonConvert.SerializeObject(model)));
                return httpReponse;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
    }
}
