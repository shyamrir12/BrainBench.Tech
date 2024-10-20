using IntegrationModels;
using IntigrationWeb.Models.Utility;
using LoginModels;
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
        public UserMasterModel AddLicensePaymentResponce(PaymentResponse paymentResponseDetails)
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

        public MessageEF GetPaymentResponseID(PaymentResponse paymentResponseDetails)
        {
            MessageEF messageEF = new MessageEF();
            try
            {
                messageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("PaymentResponses/GetPaymentResponseID", JsonConvert.SerializeObject(paymentResponseDetails)));
                return messageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
