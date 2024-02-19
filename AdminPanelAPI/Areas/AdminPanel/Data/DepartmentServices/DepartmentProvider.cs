using AdminPanelAPI.Data.ExceptionDataServices;
using AdminPanelAPI.Factory;
using AdminPanelAPI.Repository;
using AdminPanelModels;
using AdminPanelModels.UserMangment;
using Dapper;
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

        public async Task<Result<MessageEF>> AddWorkspace(Department model)
        {
            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    DepartmentName = model.deptname,
                    DepartmentNameHindi = model.deptnamehindi,
                    loginid = model.loginid,
                    ipaddress = model.ipadress,
                    Description = model.Description,
                    Image = model.Image,
                    Imagethumbnail = model.Imagethumbnail
                };


                var result = await Connection.QueryAsync<MessageEF>("Proc_Add_Department", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "AddWorkspace", Controller = "DepartmentProvider", ReturnType = "AdminPanel", UserID = "" });
                return res;
            }
            return res;
        }

        public async Task<Result<Department>> GetWorkspaceBYID(CommanRequest model)
        {
            Result<Department> res = new Result<Department>();
            try
            {
                var paramList = new
                {
                    Deptid=model.Deptid,
                    UserId = model.UserID

                };

                var result = await Connection.QueryAsync<Department>("Proc_Get_Department_ById", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "GetWorkspaceBYID", Controller = "DepartmentProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
        }

        public async Task<Result<List<Department>>> GetWorkspaceList(CommanRequest model)
        {
            Result<List<Department>> res = new Result<List<Department>>();
            try
            {
                var paramList = new
                {

                    UserId = model.UserID

                };

                var result = await Connection.QueryAsync<Department>("Proc_Get_Department_ById", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "GetWorkspaceList", Controller = "DepartmentProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
        }

        public async Task<Result<MessageEF>> ModifyStatusWorkspace(CommanRequest model)
        {
            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    Deptid=model.Deptid,
                    isactive = model.Active,
                    UserId = model.UserID


                };

                var result = await Connection.QueryAsync<MessageEF>("Proc_ModifyStatus_Department", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "ModifyStatusWorkspace", Controller = "DepartmentProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
        }

        public async Task<Result<MessageEF>> UpdateWorkspace(Department model)
        {
            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    Deptid=model.deptid,
                    DepartmentName = model.deptname,
                    DepartmentNameHindi = model.deptnamehindi,
                    loginid = model.loginid,
                    ipaddress = model.ipadress,
                    Description = model.Description,
                    Image = model.Image,
                    Imagethumbnail = model.Imagethumbnail
                };


                var result = await Connection.QueryAsync<MessageEF>("Proc_Update_Department", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "UpdateWorkspace", Controller = "DepartmentProvider", ReturnType = "AdminPanel", UserID = "" });
                return res;
            }
            return res;
        }
    }
}
