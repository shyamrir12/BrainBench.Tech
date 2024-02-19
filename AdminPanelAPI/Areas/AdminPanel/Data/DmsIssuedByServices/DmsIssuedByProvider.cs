using AdminPanelAPI.Data.ExceptionDataServices;
using AdminPanelAPI.Factory;
using AdminPanelAPI.Repository;
using AdminPanelModels;
using AdminPanelModels.UserMangment;
using Dapper;
using LoginModels;
using static System.Net.Mime.MediaTypeNames;
using System.Net;

namespace AdminPanelAPI.Areas.AdminPanel.Data.DmsIssuedByServices
{
	public class DmsIssuedByProvider : RepositoryBase, IDmsIssuedByProvider
    {
        private readonly IExceptionDataProvider _exceptionDataProvider;
        public DmsIssuedByProvider(IConnectionFactory connectionFactoryAuthDB, IExceptionDataProvider exceptionDataProvider) : base(connectionFactoryAuthDB)
        {
            _exceptionDataProvider = exceptionDataProvider;
        }

        public async Task<Result<MessageEF>> AddApplication(DmsIssuedBy model)
        {
            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                   issuedByName=model.issuedbyname ,
                   issuedByNameHindi=model.issuedbynamehindi ,
                   loginid=model.loginid,
                   ipaddress=model.ipadress,
                   Description =model.Description ,
                   Image =model.Image ,
                   Imagethumbnail =model.Imagethumbnail
                };


                var result = await Connection.QueryAsync<MessageEF>("Proc_Add_DMSIssuedby", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "AddApplication", Controller = "DmsIssuedByProvider", ReturnType = "AdminPanel", UserID = "" });
                return res;
            }
            return res;
        }

        public async Task<Result<DmsIssuedBy>> GetApplicationBYID(CommanRequest model)
        {
            Result<DmsIssuedBy> res = new Result<DmsIssuedBy>();
            try
            {
                var paramList = new
                {

                    IssuedByid =model.Iid,
                    UserId =model.UserID
                };

                var result = await Connection.QueryAsync<DmsIssuedBy>("Proc_Get_DMSIssuedBy_ById", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "GetApplicationBYID", Controller = "DmsIssuedByProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
        }

        public async Task<Result<List<DmsIssuedBy>>> GetApplicationList(CommanRequest model)
        {
            Result<List<DmsIssuedBy>> res = new Result<List<DmsIssuedBy>>();
            try
            {
                var paramList = new
                {
                    UserId=model.UserID


                };

                var result = await Connection.QueryAsync<DmsIssuedBy>("Proc_Get_DMSIssuedBy_ById", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "GetApplicationList", Controller = "DmsIssuedByProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
        }

        public async Task<Result<MessageEF>> ModifyStatusApplication(CommanRequest model)
        {
            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    issuedbyid =model.Iid,
                    isactive =model.Active,
                    UserId =model.UserID

                };

                var result = await Connection.QueryAsync<MessageEF>("Proc_ModifyStatus_DMSIssuedBy", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "ModifyStatusApplication", Controller = "DmsIssuedByProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
        }

        public async Task<Result<MessageEF>> UpdateApplication(DmsIssuedBy model)
        {
            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    issuebyid=model.issuedid,
                    issuedByName = model.issuedbyname,
                    issuedByNameHindi = model.issuedbynamehindi,
                    loginid = model.loginid,
                    ipaddress = model.ipadress,
                    Description = model.Description,
                    Image = model.Image,
                    Imagethumbnail = model.Imagethumbnail

                };


                var result = await Connection.QueryAsync<MessageEF>("Proc_Update_DMSIssuedby", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "UpdateApplication", Controller = "DmsIssuedByProvider", ReturnType = "AdminPanel", UserID = "" });
                return res;
            }
            return res;
        }
    }
}
