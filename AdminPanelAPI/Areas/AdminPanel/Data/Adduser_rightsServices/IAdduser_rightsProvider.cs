using AdminPanelModels;
using AdminPanelModels.UserMangment;
using LoginModels;

namespace AdminPanelAPI.Areas.AdminPanel.Data.Adduser_rightsServices
{
	public interface IAdduser_rightsProvider
    {
        Task<Result<List<ListItems>>> GetUserList(CommanRequest model);
        Task<Result<List<MenuItem>>> GetMenuListFormate(CommanRequest model);
        Task<Result<List<MenuItem>>> GetMenuListByRole(CommanRequest model);
        Task<Result<MessageEF>>UpdateMenuByID(CommanRequest model);


    }
}
