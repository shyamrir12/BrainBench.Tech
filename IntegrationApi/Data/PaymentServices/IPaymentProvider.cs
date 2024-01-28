using IntegrationModels;
using LoginModels;

namespace IntegrationApi.Data.PaymentServices
{
	public interface IPaymentProvider 
    {
       Task< MessageEF> AddLicenseePayment(PaymentRequestModel obj);
        // Task<PaymentModel> GetLicensePaymentGateway(LicensePaymentDetail licensePaymentDetail);
       // Task<MessageEF> InsertLicensePaymentRequest(PaymentModel model);

    }
}
