using AdminPanelAPI.Data.ExceptionDataServices;
using AdminPanelAPI.Factory;
using AdminPanelAPI.Repository;
using AdminPanelModels;
using AdminPanelModels.UserMangment;
using Dapper;
using LoginModels;


namespace AdminPanelAPI.Areas.AdminPanel.Data.DashBoardServices
{
    public class DashBoardProvider : RepositoryBase, IDashBoardProvider
    {
        private readonly IExceptionDataProvider _exceptionDataProvider;
        public DashBoardProvider (IConnectionFactory connectionFactoryAuthDB, IExceptionDataProvider exceptionDataProvider) : base(connectionFactoryAuthDB)
        {
            _exceptionDataProvider = exceptionDataProvider;
        }
        public async Task<Result<DashboardModel>> GetDashboard(CommanRequest model)
        {
            Result<DashboardModel> res = new Result<DashboardModel>();
            try
            {
                var paramList = new
                {
                    UserID = model.UserID,
                    Activation = model.Active,
                    SubRoleId = model.SubRoleId,
                    Check = 3,

                };

                var result = await Connection.QueryAsync<DashboardModel>("Proc_Dashboard", paramList, commandType: System.Data.CommandType.StoredProcedure);

                if (result.Count() > 0)
                {

                    res.Data = result.FirstOrDefault();
                    res.Status = true;
                    res.Message = new List<string>() { "Successful!" };
                }
                else
                {
                    res.Data = null;
                    res.Status = false;
                    res.Message = new List<string>() { "Failed!" };

                }
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = false;
                res.Message.Add("Exception Occur! - " + ex.Message.ToString());
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "GetDashboard", Controller = "DashBoardProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
        }
    }
}
