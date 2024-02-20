using AdminPanelAPI.Areas.AdminPanel.Data.LicenseServices;
using AdminPanelModels;
using AdminPanelModels.UserMangment;
using LoginModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelAPI.Areas.AdminPanel.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LicenseController : ControllerBase
	{
		private readonly ILicenseProvider _licenseProvider;

		public LicenseController(ILicenseProvider licenseProvider)
		{
			_licenseProvider = licenseProvider;
		}
        public Task<Result<List<ListItems>>> GetLicenseTypeList(CommanRequest model)
        {
            return _licenseProvider.GetLicenseTypeList(model);
        }
        public Task<Result<List<LicenseModel>>> GetLicenseList(CommanRequest model)
        {
            return _licenseProvider.GetLicenseList(model);
        }
        public Task<Result<List<LicenseTranModel>>> GetLicenseTranList(CommanRequest model)
        {
            return _licenseProvider.GetLicenseTranList(model);
        }
        public Task<Result<LicenseModel>> GetLicenseBYID(CommanRequest model)
        {
            return _licenseProvider.GetLicenseBYID(model);
        }
        public Task<Result<MessageEF>> AddUpdateLicense(LicenseModel model)
        {
            return _licenseProvider.AddUpdateLicense(model);
        }
        public Task<Result<MessageEF>> ModifyStatusLicense(CommanRequest model)
        {
            return _licenseProvider.ModifyStatusLicense(model);
        }
        public Task<Result<MessageEF>> SubscribeLicense(LicenseTranModel model)
        {
            return _licenseProvider.SubscribeLicense(model);
        }
        public Task<Result<MessageEF>> UnSubscribeLicense(CommanRequest model)
        {
            return _licenseProvider.UnSubscribeLicense(model);
        }
        public Task<Result<MessageEF>> UpdatePaymentStatus(LicenseTranModel model)
        {
            return _licenseProvider.UpdatePaymentStatus(model);
        }


    }
}
