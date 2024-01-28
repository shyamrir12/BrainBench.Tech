using IntegrationApi.Data.PaymentResponsesServices;
using IntegrationApi.Data.PaymentServices;
using IntegrationModels;
using LoginModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentController : ControllerBase
	{
        private readonly IPaymentProvider _objILicenseeProvider;
        public PaymentController(IPaymentProvider objILicenseeProvider)
        {
            _objILicenseeProvider = objILicenseeProvider;
        }
        /// <summary>
        /// Get License Payment Details
        /// </summary>
        /// <param name="licensePaymentDetail"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageEF> AddLicenseePayment(PaymentRequestModel obj)
        {
            return await _objILicenseeProvider.AddLicenseePayment(obj);
        }
    }
}
