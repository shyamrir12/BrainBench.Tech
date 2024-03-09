using AdminPanelModels;
using LoginModels;

namespace AdminPanelAPI.Areas.AdminPanel.Data.UserMappingServices
{
	public interface IUserMappingProvider
    {
        Task<Result<List<ListItems>>> GetApplication(CommanRequest model);
        Task<Result<List<ListItems>>> GetWorkspace(CommanRequest model);
        Task<Result<List<ListItems>>> GetOutlet(CommanRequest model);
        Task<Result<MessageEF>> UpdateUserMapping(CommanRequest model);
       
    }
}
