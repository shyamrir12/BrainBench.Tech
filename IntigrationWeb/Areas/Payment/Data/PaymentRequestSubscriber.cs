using IntegrationModels;
using IntigrationWeb.Models.Utility;
using LoginModels;
using Newtonsoft.Json;

namespace IntigrationWeb.Areas.Payment.Data
{
    public class PaymentRequestSubscriber : IPaymentRequestSubscriber
    {
        private readonly IHttpWebClients _objIHttpWebClients;
        public PaymentRequestSubscriber(IHttpWebClients objIHttpWebClients)
        {

            _objIHttpWebClients = objIHttpWebClients;
        }
        public MessageEF AddLicensePayment(PaymentRequestModel obj)
        {
            try
            {
                return JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("Payment/AddLicensePayment", JsonConvert.SerializeObject(obj)));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PaymentModel GetPaymentGateway(PaymentRequestModel licensePaymentDetail)
        {
            try
            {
                return JsonConvert.DeserializeObject<PaymentModel>(_objIHttpWebClients.PostRequest("Payment/GetPaymentGateway", JsonConvert.SerializeObject(licensePaymentDetail)));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public MessageEF InsertPaymentRequest(PaymentModel model)
        {
            try
            {
                return JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("Payment/InsertPaymentRequest", JsonConvert.SerializeObject(model)));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
