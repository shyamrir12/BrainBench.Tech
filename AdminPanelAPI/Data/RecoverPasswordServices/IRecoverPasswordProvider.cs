
using AdminPanelModels;
using LoginModels;

namespace AdminPanelAPI.Data.RecoverPasswordServices
{
	public interface IRecoverPasswordProvider
    {
        Task<Result<MessageEF>> GetRecoverPassword(RecoverPassword model);
    }
}
