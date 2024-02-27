using AdminPanelAPI.Data.ExceptionDataServices;
using AdminPanelAPI.Factory;
using AdminPanelAPI.Repository;
using AdminPanelModels;
using AdminPanelModels.UserMangment;
using Dapper;
using LoginModels;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data;
using System.Net;
using System.Reflection;

namespace AdminPanelAPI.Areas.AdminPanel.Data.AddUserServices
{
    public class AddUserProvider :RepositoryBase, IAddUserProvider
    {
        private readonly IExceptionDataProvider _exceptionDataProvider;
        public AddUserProvider(IConnectionFactory connectionFactoryAuthDB, IExceptionDataProvider exceptionDataProvider) : base(connectionFactoryAuthDB)
        {
            _exceptionDataProvider= exceptionDataProvider;
        }
        public async Task<Result<MessageEF>> ActivationUser(CommanRequest model)
        {
            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    UserID = model.UserID,
                    Activation = model.Active,
                    SubRoleId = model.SubRoleId,
                    Check = 3,

                };

                var result = await Connection.QueryAsync<MessageEF>("AdminPane_User_Master", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "ActivationUser", Controller = "AddUserProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
        }

        public async Task<Result<MessageEF>> AddUpdateUser(AddUserModel model)
        {
           // model.PD_Reenterpwd = MyUtility.ComputeSha256Hash(model.PD_Reenterpwd);

            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    UserID  = model.userid,
                    RoleId  = model.roleval,
                    Name  = model.name,
                    //Ipaddress  = model.Ipaddress,
                    Designation  = model.designation,
                    Email  = model.EmailId,
                    Mobile  = model.mobile_no,
                    Check =model.userid==null? 1: 2,

                };


                var result = await Connection.QueryAsync<MessageEF>("AdminPane_User_Master", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "AddUpdateUser", Controller = "AddUserProvider", ReturnType = "AdminPanel", UserID = model.userid.ToString() });
                return res;
            }
            return res;
        }

        public async Task<Result<List<ListItems>>> GetRole(CommanRequest model)
        {
            Result<List<ListItems>> res = new Result<List<ListItems>>();
            try
            {
                var paramList = new
                {
                    UserID = model.UserID,
                    Check = 1,

                };

                var result = await Connection.QueryAsync<ListItems>("Proc_Get_All_DropDown", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry {ErrorMessage=ex.Message,StackTrace=ex.StackTrace,Action= "GetRole",Controller= "AddUserProvider", ReturnType="AdminPanel",UserID=model.UserID.ToString() });
                return res;
            }
            return res;
        }

        public async Task<Result<AddUserModel>> GetUserByID(CommanRequest model)
        {
            Result<AddUserModel> res = new Result<AddUserModel>();
            try
            {
                var paramList = new
                {
                    UserID = model.UserID,
                   

                };

                var result = await Connection.QueryAsync<AddUserModel>("Proc_Get_All_User", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "GetUserByID", Controller = "AddUserProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
        }

        public async Task<Result<List<AddUserModel>>> GetUserList(CommanRequest model)
        {
            Result<List<AddUserModel>> res = new Result<List<AddUserModel>>();
            int TotalRecord = 0;
            try
            {
               
                var paramList = new DynamicParameters();
                paramList.Add("UserID", model.UserID);
                paramList.Add("PageSize", model.PageSize);
                paramList.Add("PageIndex", model.PageIndex);
                paramList.Add("Filter", model.Filter);
                paramList.Add("TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var result = await Connection.QueryAsync<AddUserModel>("Proc_Get_All_User", paramList, commandType: System.Data.CommandType.StoredProcedure);
                 TotalRecord = paramList.Get<int>("TotalRecord");

                if (result.Count() > 0)
                {

                    res.Data = result.ToList();
                    res.Status = true;
                    res.Message = new List<string>() { "Successful!", TotalRecord.ToString() };
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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "GetUserList", Controller = "AddUserProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
        }

        public async Task<Result<MessageEF>> UpdatePassword(CommanRequest model)
        {
            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    UserID = model.UserID,
                    OldPassword = model.OldPassword,
                    Password = model.Password,
                    Check = 2
                   

                };


                var result = await Connection.QueryAsync<MessageEF>("changepassword", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "UpdatePassword", Controller = "AddUserProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
        }
    }
}
