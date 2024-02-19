using AdminPanelModels.UserMangment;
using AdminPanelModels;
using LoginModels;

namespace AdminPanelAPI.Areas.AdminPanel.Data.DepartmentServices
{
	public interface IDepartmentProvider
	{
        Task<Result<List<Department>>> GetWorkspaceList(CommanRequest model);
        Task<Result<Department>> GetWorkspaceBYID(CommanRequest model);
        Task<Result<MessageEF>> AddWorkspace(Department model);
        Task<Result<MessageEF>> UpdateWorkspace(Department model);
        Task<Result<MessageEF>> ModifyStatusWorkspace(CommanRequest model);
    }
}
