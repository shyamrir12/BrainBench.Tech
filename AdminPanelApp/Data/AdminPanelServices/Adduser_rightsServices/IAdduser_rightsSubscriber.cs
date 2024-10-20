using AdminPanelModels;
using LoginModels;

namespace AdminPanelApp.Data.AdminPanelServices.Adduser_rightsServices
{
    public interface IAdduser_rightsSubscriber
    {
        Task<Result<List<ListItems>>> GetUserList(CommanRequest model);
        Task<Result<List<MenuItem>>> GetMenuListFormate(CommanRequest model);
        Task<Result<List<MenuItem>>> GetMenuListByRole(CommanRequest model);
        Task<Result<MessageEF>> UpdateMenuByID(CommanRequest model);

    }
}
