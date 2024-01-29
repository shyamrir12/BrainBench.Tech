using IntegrationModels;

namespace IntigrationWeb.Models.PaymentResponsesService
{
    public interface IPaymentResponsesSubscriber
    {
        UserMasterModel AddLicenseResponcePayment(PaymentResponse paymentResponseDetails);
    }
}
