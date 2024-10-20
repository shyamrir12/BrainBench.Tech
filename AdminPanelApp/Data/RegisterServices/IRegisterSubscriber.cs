using AdminPanelModels;
using LoginModels;

namespace AdminPanelApp.Data.RegisterServices
{
	public interface IRegisterSubscriber
	{
        Task<Result<MessageEF>> RegisterUser(RegisterUser model);
        Task<Result<MessageEF>> CheckUserExist(RegisterUser model);
        Task<Result<List<ListItems>>> GetApplicationType();
    }
}
