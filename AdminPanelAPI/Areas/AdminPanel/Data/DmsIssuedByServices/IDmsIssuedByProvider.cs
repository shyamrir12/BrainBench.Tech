using AdminPanelModels.UserMangment;
using AdminPanelModels;
using LoginModels;

namespace AdminPanelAPI.Areas.AdminPanel.Data.DmsIssuedByServices
{
	public interface IDmsIssuedByProvider
	{
        Task<Result<List<DmsIssuedBy>>> GetApplicationList(CommanRequest model);
        Task<Result<DmsIssuedBy>> GetApplicationBYID(CommanRequest model);
        Task<Result<MessageEF>> AddApplication(DmsIssuedBy model);
        Task<Result<MessageEF>> UpdateApplication(DmsIssuedBy model);
        Task<Result<MessageEF>> ModifyStatusApplication(CommanRequest model);

    }
}
