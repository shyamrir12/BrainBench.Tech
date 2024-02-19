using AdminPanelModels;
using LoginModels;

namespace AdminPanelAPI.Areas.AdminPanel.Data.UserMappingServices
{
	public interface IUserMappingProvider
    {
        Task<Result<List<ListItems>>> GetApplication(CommanRequest model);
        Task<Result<List<ListItems>>> GetWorkspace(CommanRequest model);
        Task<Result<List<ListItems>>> GetOutlet(CommanRequest model);
        Task<Result<MessageEF>> AssignApplication(CommanRequest model);
        Task<Result<MessageEF>> AssignWorkspace(CommanRequest model);
        Task<Result<MessageEF>> AssignOutlet(CommanRequest model);
    }
}
