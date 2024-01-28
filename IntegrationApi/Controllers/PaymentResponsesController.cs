using IntegrationApi.Data.PaymentResponsesServices;
using IntegrationModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentResponsesController : ControllerBase
    {
        private readonly IPaymentResponsesProvider _paymentResponseProvider;

        public PaymentResponsesController(IPaymentResponsesProvider paymentResponseProvider)
        {
            _paymentResponseProvider = paymentResponseProvider;
        }
        [HttpPost]
        public async Task<UserMasterModel> AddLicenseePaymentResponse(PaymentResponse paymentResponseDetails)
        {
            return await _paymentResponseProvider.AddLicenseePaymentResponse(paymentResponseDetails);
        }
    }
}
