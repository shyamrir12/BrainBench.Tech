using IntegrationModels;
using LoginModels;

namespace IntigrationWeb.Models.PaymentResponsesService
{
    public interface IPaymentResponsesSubscriber
    {
        MessageEF GetPaymentResponseID(PaymentResponse paymentResponseDetails);
        UserMasterModel AddLicensePaymentResponce(PaymentResponse paymentResponseDetails);
    }
}
