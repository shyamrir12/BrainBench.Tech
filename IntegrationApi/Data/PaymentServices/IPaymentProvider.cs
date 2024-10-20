using IntegrationModels;
using LoginModels;

namespace IntegrationApi.Data.PaymentServices
{
	public interface IPaymentProvider 
    {
       Task< MessageEF> AddLicensePayment(PaymentRequestModel obj);
         Task<PaymentModel> GetPaymentGateway(PaymentRequestModel PaymentDetail);
        Task<MessageEF> InsertPaymentRequest(PaymentModel model);

    }
}
