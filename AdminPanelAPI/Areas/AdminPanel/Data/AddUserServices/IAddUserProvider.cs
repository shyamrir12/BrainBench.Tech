using AdminPanelModels;
using AdminPanelModels.UserMangment;
using LoginModels;

namespace AdminPanelAPI.Areas.AdminPanel.Data.AddUserServices
{
	public interface IAddUserProvider
    {
        Task<Result<MessageEF>> AddUpdateUser(AddUserModel model);
        Task<Result<MessageEF>> ActivationUser(CommanRequest model);
        Task<Result<List<AddUserModel>>> GetUserList(CommanRequest model);
        Task<Result<AddUserModel>> GetUserByID(CommanRequest model);
        Task<Result<List<ListItems>>> GetRole(CommanRequest model);
        Task<Result<MessageEF>> UpdatePassword(CommanRequest model);
       
    }
}
