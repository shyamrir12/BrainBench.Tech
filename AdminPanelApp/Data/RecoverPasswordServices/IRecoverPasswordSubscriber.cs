using AdminPanelModels;
using LoginModels;

namespace AdminPanelApp.Data.RecoverPasswordServices
{
    public interface IRecoverPasswordSubscriber
    {
        Task<Result<MessageEF>> GetRecoverPassword(RecoverPassword model);
    }
}
