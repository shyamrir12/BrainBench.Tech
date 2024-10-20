using AdminPanelModels;
using LoginModels;

namespace AdminPanelApp.Data.AdminPanelServices.UserMappingServices
{
    public interface IUserMappingSubscriber
    {
        Task<Result<List<ListItems>>> GetApplication(CommanRequest model);
        Task<Result<List<ListItems>>> GetWorkspace(CommanRequest model);
        Task<Result<List<ListItems>>> GetOutlet(CommanRequest model);
        Task<Result<MessageEF>> UpdateUserMapping(CommanRequest model);
      
    }
}
