using AdminPanelAPI.Data.ExceptionDataServices;
using AdminPanelAPI.Factory;
using AdminPanelAPI.Repository;
using AdminPanelModels;
using AdminPanelModels.UserMangment;
using LoginModels;

namespace AdminPanelAPI.Areas.AdminPanel.Data.DepartmentServices
{
    public class DepartmentProvider : RepositoryBase, IDepartmentProvider

    {

        private readonly IExceptionDataProvider _exceptionDataProvider;
        public DepartmentProvider(IConnectionFactory connectionFactoryAuthDB, IExceptionDataProvider exceptionDataProvider) : base(connectionFactoryAuthDB)
        {
            _exceptionDataProvider = exceptionDataProvider;
        }

        public Task<Result<MessageEF>> AddWorkspace(Department model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Department>> GetWorkspaceBYID(CommanRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<Department>>> GetWorkspaceList(CommanRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<MessageEF>> ModifyStatusWorkspace(CommanRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<MessageEF>> UpdateWorkspace(Department model)
        {
            throw new NotImplementedException();
        }
    }
}
