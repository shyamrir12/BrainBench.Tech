using AdminPanelModels.UserMangment;
using AdminPanelModels;
using LoginModels;
using System.ComponentModel;

namespace AdminPanelAPI.Areas.AdminPanel.Data.LicenseServices
{
	public interface ILicenseProvider
    {
         Task<Result<List<ListItems>>> GetLicenseTypeList(CommanRequest model);
        Task<Result<List<LicenseModel>>> GetLicenseList(CommanRequest model);
        Task<Result<List<LicenseTranModel>>> GetLicenseTranList(CommanRequest model);
        Task<Result<LicenseModel>> GetLicenseBYID(CommanRequest model);
        Task<Result<MessageEF>> AddUpdateLicense(LicenseModel model);
        Task<Result<MessageEF>> ModifyStatusLicense(CommanRequest model);
        Task<Result<MessageEF>> SubscribeLicense(LicenseTranModel model);
        Task<Result<MessageEF>> UnSubscribeLicense(CommanRequest model);
        Task<Result<MessageEF>> UpdatePaymentStatus(CommanRequest model);


    }
}
