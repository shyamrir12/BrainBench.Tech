
using AdminPanelModels;
using LoginModels;

namespace AdminPanelAPI.Data.RecoverPasswordServices
{
	public interface IRecoverPasswordProvider
    {
        Task<Result<string>> RecoverPassword(RecoverPassword model);
    }
}
