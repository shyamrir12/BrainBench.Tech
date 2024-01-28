using IntegrationModels;

namespace IntegrationApi.Data.PaymentResponsesServices
{
	public interface IPaymentResponsesProvider
    {
        Task<UserMasterModel> AddLicenseePaymentResponse(PaymentResponse paymentResponseDetails);
    }
}
