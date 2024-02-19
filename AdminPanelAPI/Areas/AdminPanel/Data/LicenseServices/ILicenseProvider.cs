using AdminPanelModels.UserMangment;
using AdminPanelModels;
using LoginModels;
using System.ComponentModel;

namespace AdminPanelAPI.Areas.AdminPanel.Data.LicenseServices
{
	public interface ILicenseProvider
    {
        Task<Result<List<License>>> GetLicenseList(CommanRequest model);
        Task<Result<License>> GetLicenseBYID(CommanRequest model);
        Task<Result<MessageEF>> AddUpdateLicense(LicenseModel model);
        Task<Result<MessageEF>> ModifyStatusLicense(CommanRequest model);
        Task<Result<MessageEF>> SubscribeLicense(LicenseTranModel model);
        Task<Result<MessageEF>> UnSubscribeLicense(CommanRequest model);
        Task<Result<MessageEF>> UpdatePaymnetStatus(CommanRequest model);

    }
}
