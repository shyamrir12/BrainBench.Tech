using AdminPanelAPI.Factory;
using AdminPanelAPI.Repository;
using AdminPanelModels;
using LoginModels;

namespace AdminPanelAPI.Data.RegisterServices
{
    public class RegisterProvider :RepositoryBase, IRegisterProvider
    {
        protected RegisterProvider(IConnectionFactory connectionFactoryAuthDB) : base(connectionFactoryAuthDB)
        {
        }

        public Task<Result<string>> RegisterUser(RegisterUser model)
        {


            throw new NotImplementedException();
        }
    }
}
