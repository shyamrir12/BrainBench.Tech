using AdminPanelAPI.Data.ExceptionDataServices;
using AdminPanelAPI.Factory;
using AdminPanelAPI.Repository;
using AdminPanelModels;
using AdminPanelModels.UserMangment;
using LoginModels;

namespace AdminPanelAPI.Areas.AdminPanel.Data.DmsHECategoryServices
{
    public class DmsHECategoryProvider : RepositoryBase, IDmsHECategoryProvider

    {
        private readonly IExceptionDataProvider _exceptionDataProvider;
        public DmsHECategoryProvider(IConnectionFactory connectionFactoryAuthDB, IExceptionDataProvider exceptionDataProvider) : base(connectionFactoryAuthDB)
        {
            _exceptionDataProvider = exceptionDataProvider;
        }

        public Task<Result<MessageEF>> AddOutlet(DmsHECategory model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DmsHECategory>> GetOutletBYID(CommanRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<DmsHECategory>>> GetOutletList(CommanRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<MessageEF>> ModifyStatusOutlet(CommanRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<MessageEF>> UpdateOutlet(DmsHECategory model)
        {
            throw new NotImplementedException();
        }
    }
}
