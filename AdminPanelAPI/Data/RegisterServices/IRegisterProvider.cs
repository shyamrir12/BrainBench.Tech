using AdminPanelModels;
using LoginModels;

namespace AdminPanelAPI.Data.RegisterServices
{
	public interface IRegisterProvider
	{
        Task<Result<string>> RegisterUser(RegisterUser model);
    }
}
