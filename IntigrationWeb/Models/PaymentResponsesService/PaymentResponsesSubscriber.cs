using IntegrationModels;
using IntigrationWeb.Models.Utility;
using Newtonsoft.Json;

namespace IntigrationWeb.Models.PaymentResponsesService
{
    public class PaymentResponsesSubscriber : IPaymentResponsesSubscriber
    {
        private readonly IHttpWebClients _objIHttpWebClients;
        public PaymentResponsesSubscriber(IHttpWebClients objIHttpWebClients)
        {

            _objIHttpWebClients = objIHttpWebClients;
        }
        public UserMasterModel AddLicenseResponcePayment(PaymentResponse paymentResponseDetails)
        {
            UserMasterModel userMasterModel = new UserMasterModel();
            try
            {
                userMasterModel = JsonConvert.DeserializeObject<UserMasterModel>(_objIHttpWebClients.PostRequest("PaymentResponses/AddLicenseResponcePayment", JsonConvert.SerializeObject(paymentResponseDetails)));
                return  userMasterModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
