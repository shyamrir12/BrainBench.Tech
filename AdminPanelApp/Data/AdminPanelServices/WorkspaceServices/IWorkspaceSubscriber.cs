using AdminPanelModels.UserMangment;
using AdminPanelModels;
using LoginModels;

namespace AdminPanelApp.Data.AdminPanelServices.WorkspaceServices
{
    public interface IWorkspaceSubscriber
    {
        Task<Result<List<Department>>> GetWorkspaceList(CommanRequest model);
        Task<Result<Department>> GetWorkspaceBYID(CommanRequest model);
        Task<Result<MessageEF>> AddWorkspace(Department model);
        Task<Result<MessageEF>> UpdateWorkspace(Department model);
        Task<Result<MessageEF>> ModifyStatusWorkspace(CommanRequest model);
    }
}
