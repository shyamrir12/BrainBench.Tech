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
        private readonly IPaymentProvider _objILicenseProvider;
        public PaymentController(IPaymentProvider objILicenseeProvider)
        {
            _objILicenseProvider = objILicenseeProvider;
        }
      
        [HttpPost]
        public async Task<MessageEF> AddLicensePayment(PaymentRequestModel obj)
        {
            return await _objILicenseProvider.AddLicensePayment(obj);
        }
        [HttpPost]
        public async Task<PaymentModel> GetPaymentGateway(PaymentRequestModel licensePaymentDetail)
        {
            return await _objILicenseProvider.GetPaymentGateway(licensePaymentDetail);
        }

      
        [HttpPost]
        public async Task<MessageEF> InsertPaymentRequest(PaymentModel model)
        {
            return await _objILicenseProvider.InsertPaymentRequest(model);
        }
    }
}
