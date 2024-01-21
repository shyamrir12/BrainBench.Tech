using AdminPanelModels;
using LoginModels;

namespace AdminPanelAPI.Data.RegisterServices
{
	public interface IRegisterProvider
	{
        Task<Result<MessageEF>> RegisterUser(RegisterUser model);
        Task<Result<MessageEF>> CheckUserExist(RegisterUser model);
        Task<Result<List<ListItems>>> GetApplicationType();
    }
}
