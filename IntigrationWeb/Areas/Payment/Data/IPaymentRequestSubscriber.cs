using IntegrationModels;
using LoginModels;

namespace IntigrationWeb.Areas.Payment.Data
{
    public interface IPaymentRequestSubscriber
    {
        MessageEF AddLicensePayment(PaymentRequestModel obj);
        PaymentModel GetPaymentGateway(PaymentRequestModel licensePaymentDetail);
        MessageEF InsertPaymentRequest(PaymentModel model);
    }
}
