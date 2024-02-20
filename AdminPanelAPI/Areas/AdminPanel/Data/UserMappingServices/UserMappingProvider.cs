using AdminPanelAPI.Data.ExceptionDataServices;
using AdminPanelAPI.Factory;
using AdminPanelAPI.Repository;
using AdminPanelModels;
using Dapper;
using LoginModels;

namespace AdminPanelAPI.Areas.AdminPanel.Data.UserMappingServices
{
    public class UserMappingProvider : RepositoryBase, IUserMappingProvider

    {
        private readonly IExceptionDataProvider _exceptionDataProvider;
        public UserMappingProvider(IConnectionFactory connectionFactoryAuthDB, IExceptionDataProvider exceptionDataProvider) : base(connectionFactoryAuthDB)
        {
            _exceptionDataProvider = exceptionDataProvider;
        }

        public async Task<Result<MessageEF>> AssignApplication(CommanRequest model)
        {
            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    loginId = model.SubRoleId,
                    UserID = model.UserID,
                    Iids = model.ids
                };
                var result = await Connection.QueryAsync<MessageEF>("Proc_Insert_UserMapping", paramList, commandType: System.Data.CommandType.StoredProcedure);

                if (result.Count() > 0)
                {

                    res.Data = result.FirstOrDefault();
                    res.Status = true;
                    res.Message = new List<string>() { "Successful!" };
                }
                else
                {
                    res.Data = null;
                    res.Status = true;
                    res.Message = new List<string>() { "Failed!" };

                }
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Message.Add("Exception Occurred! - " + ex.Message.ToString());
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "AssignApplication", Controller = "UserMappingProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });

                return res;
            }
            return res;
        }

        public async Task<Result<MessageEF>> AssignOutlet(CommanRequest model)
        {
            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    loginId = model.SubRoleId,
                    UserID = model.UserID,
                    Cids = model.ids
                };
                var result = await Connection.QueryAsync<MessageEF>("Proc_Insert_UserMapping", paramList, commandType: System.Data.CommandType.StoredProcedure);

                if (result.Count() > 0)
                {

                    res.Data = result.FirstOrDefault();
                    res.Status = true;
                    res.Message = new List<string>() { "Successful!" };
                }
                else
                {
                    res.Data = null;
                    res.Status = true;
                    res.Message = new List<string>() { "Failed!" };

                }
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Message.Add("Exception Occurred! - " + ex.Message.ToString());
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "AssignOutlet", Controller = "UserMappingProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });

                return res;
            }
            return res;
        }

        public async Task<Result<MessageEF>> AssignWorkspace(CommanRequest model)
        {
            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    loginId = model.SubRoleId,
                    UserID = model.UserID,
                    Deptids = model.ids
                };
                var result = await Connection.QueryAsync<MessageEF>("Proc_Insert_UserMapping", paramList, commandType: System.Data.CommandType.StoredProcedure);

                if (result.Count() > 0)
                {

                    res.Data = result.FirstOrDefault();
                    res.Status = true;
                    res.Message = new List<string>() { "Successful!" };
                }
                else
                {
                    res.Data = null;
                    res.Status = true;
                    res.Message = new List<string>() { "Failed!" };

                }
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Message.Add("Exception Occurred! - " + ex.Message.ToString());
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "AssignWorkspace", Controller = "UserMappingProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });

                return res;
            }
            return res;
        }

        public async Task<Result<List<ListItems>>> GetApplication(CommanRequest model)
        {
            Result<List<ListItems>> res = new Result<List<ListItems>>();
            try
            {

                var paramList = new
                {
                    UserID = model.UserID,
                    Check = 1,

                };
                var result = await Connection.QueryAsync<ListItems>("Get_TBL_Document_IssueBy_Master", paramList, commandType: System.Data.CommandType.StoredProcedure);

                if (result.Count() > 0)
                {

                    res.Data = result.ToList();
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
                return res;
            }
            return res;
        }

        public async Task<Result<List<ListItems>>> GetOutlet(CommanRequest model)
        {
            Result<List<ListItems>> res = new Result<List<ListItems>>();
            try
            {

                var paramList = new
                {
                    UserID = model.UserID,
                    Check = 3,

                };
                var result = await Connection.QueryAsync<ListItems>("Get_TBL_Document_IssueBy_Master", paramList, commandType: System.Data.CommandType.StoredProcedure);

                if (result.Count() > 0)
                {

                    res.Data = result.ToList();
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
                return res;
            }
            return res;
        }

        public async Task<Result<List<ListItems>>> GetWorkspace(CommanRequest model)
        {
            Result<List<ListItems>> res = new Result<List<ListItems>>();
            try
            {

                var paramList = new
                {
                    UserID = model.UserID,
                    Check = 2,

                };
                var result = await Connection.QueryAsync<ListItems>("Get_TBL_Document_IssueBy_Master", paramList, commandType: System.Data.CommandType.StoredProcedure);

                if (result.Count() > 0)
                {

                    res.Data = result.ToList();
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
                return res;
            }
            return res;
        }
    }
}
