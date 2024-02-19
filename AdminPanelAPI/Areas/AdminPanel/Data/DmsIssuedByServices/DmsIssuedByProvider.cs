using AdminPanelAPI.Data.ExceptionDataServices;
using AdminPanelAPI.Factory;
using AdminPanelAPI.Repository;
using AdminPanelModels;
using AdminPanelModels.UserMangment;
using LoginModels;

namespace AdminPanelAPI.Areas.AdminPanel.Data.DmsIssuedByServices
{
	public class DmsIssuedByProvider : RepositoryBase, IDmsIssuedByProvider
    {
        private readonly IExceptionDataProvider _exceptionDataProvider;
        public DmsIssuedByProvider(IConnectionFactory connectionFactoryAuthDB, IExceptionDataProvider exceptionDataProvider) : base(connectionFactoryAuthDB)
        {
            _exceptionDataProvider = exceptionDataProvider;
        }

        public Task<Result<MessageEF>> AddApplication(DmsIssuedBy model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DmsIssuedBy>> GetApplicationBYID(CommanRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<DmsIssuedBy>>> GetApplicationList(CommanRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<MessageEF>> ModifyStatusApplication(CommanRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<MessageEF>> UpdateApplication(DmsIssuedBy model)
        {
            throw new NotImplementedException();
        }
    }
}
