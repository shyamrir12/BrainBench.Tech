using IntegrationModels;
using LoginModels;

namespace IntegrationApi.Data.PaymentResponsesServices
{
	public interface IPaymentResponsesProvider
    {
        Task<MessageEF> GetPaymentResponseID(PaymentResponse paymentResponseDetails);
        Task<UserMasterModel> AddLicenseePaymentResponse(PaymentResponse paymentResponseDetails);
       
    }
}
