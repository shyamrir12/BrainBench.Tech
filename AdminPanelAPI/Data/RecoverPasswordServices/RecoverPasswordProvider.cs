using AdminPanelAPI.Factory;
using AdminPanelAPI.Repository;
using AdminPanelModels;
using LoginModels;

namespace AdminPanelAPI.Data.RecoverPasswordServices
{
    public class RecoverPasswordProvider : RepositoryBase, IRecoverPasswordProvider
    {
        protected RecoverPasswordProvider(IConnectionFactory connectionFactoryAuthDB) : base(connectionFactoryAuthDB)
        {
        }

        public Task<Result<string>> RecoverPassword(RecoverPassword model)
        {
            throw new NotImplementedException();
        }
    }
}
