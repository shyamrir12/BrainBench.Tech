using AdminPanelAPI.Data.ExceptionDataServices;
using AdminPanelAPI.Factory;
using AdminPanelAPI.Repository;
using AdminPanelModels;
using AdminPanelModels.UserMangment;
using Dapper;
using LoginModels;
using System.ComponentModel;

namespace AdminPanelAPI.Areas.AdminPanel.Data.LicenseServices
{
    public class LicenseProvider : RepositoryBase, ILicenseProvider
    {
        private readonly IExceptionDataProvider _exceptionDataProvider;

        public LicenseProvider(IConnectionFactory connectionFactoryAuthDB, IExceptionDataProvider exceptionDataProvider) : base(connectionFactoryAuthDB)
        {
            _exceptionDataProvider = exceptionDataProvider;
        }

        public async Task<Result<MessageEF>> AddUpdateLicense(LicenseModel model)
        {
            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    Check=model.LicenseID==null ||model.LicenseID==0?2:3
                };


                var result = await Connection.QueryAsync<MessageEF>("Proc_CRUD_Licensee_Master", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "AddUpdateLicense", Controller = "LicenseProvider", ReturnType = "AdminPanel", UserID = "" });
                return res;
            }
            return res;
        }

        public async Task<Result<LicenseModel>> GetLicenseBYID(CommanRequest model)
        {
            Result<LicenseModel> res = new Result<LicenseModel>();
            try
            {
                var paramList = new
                {
                    LicenseID = model.id,
                    UserId = model.UserID,
                    Check=1

                };

                var result = await Connection.QueryAsync<LicenseModel>("Proc_CRUD_Licensee_Master", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "GetLicenseBYID", Controller = "LicenseProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
        }
        public async Task<Result<List<LicenseModel>>> GetLicenseList(CommanRequest model)
        {
            Result<List<LicenseModel>> res = new Result<List<LicenseModel>>();
            try
            {
                var paramList = new
                {
                    
                    UserId = model.UserID,//for admin
                    Check = 1

                };

                var result = await Connection.QueryAsync<LicenseModel>("Proc_CRUD_Licensee_Master", paramList, commandType: System.Data.CommandType.StoredProcedure);

                if (result.Count() > 0)
                {

                    res.Data = result.ToList(); ;
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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "GetLicenseList", Controller = "LicenseProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
        }

        public async Task<Result<List<LicenseTranModel>>> GetLicenseTranList(CommanRequest model)
        {
            Result<List<LicenseTranModel>> res = new Result<List<LicenseTranModel>>();
            try
            {
                var paramList = new
                {

                    UserId = model.UserID,//for other user
                    Check = 1

                };

                var result = await Connection.QueryAsync<LicenseTranModel>("Proc_CRUD_Licensee_Master", paramList, commandType: System.Data.CommandType.StoredProcedure);

                if (result.Count() > 0)
                {

                    res.Data = result.ToList(); ;
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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "GetLicenseTranList", Controller = "LicenseProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
        }

        public async Task<Result<List<ListItems>>> GetLicenseTypeList(CommanRequest model)
        {
            Result<List<ListItems>> res = new Result<List<ListItems>>();
            try
            {

                var paramList = new
                {
                    UserID = model.UserID,
                    Check = 5,

                };
                var result = await Connection.QueryAsync<ListItems>(" Proc_Get_All_DropDown", paramList, commandType: System.Data.CommandType.StoredProcedure);

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

        public async Task<Result<MessageEF>> ModifyStatusLicense(CommanRequest model)
        {
            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    LicenseID = model.id,
                    isactive = model.Active,
                    UserId = model.UserID,
                    Check = 4,

                };

                var result = await Connection.QueryAsync<MessageEF>("Proc_CRUD_Licensee_Master", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "ModifyStatusLicense", Controller = "LicenseProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
        }

        public async Task<Result<MessageEF>> SubscribeLicense(LicenseTranModel model)
        {
            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    LicenseID=model.LicenseID,
                    Certificate=model.Certificate,
                    UserID = model.CreatedByUserID,               
                    PaymentStatus=model.PaymentStatus,
                    PaymentDate=model.PaymentDate,
                    PaymentRefNo=model.PaymentRefNo,
                    Check = 5,

                };

                var result = await Connection.QueryAsync<MessageEF>("Proc_CRUD_Licensee_Master", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "SubscribeLicense", Controller = "LicenseProvider", ReturnType = "AdminPanel", UserID = model.CreatedByUserID.ToString() });
                return res;
            }
            return res;
        }

        public async Task<Result<MessageEF>> UnSubscribeLicense(CommanRequest model)
        {
            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    LicenseTransactionID = model.id,
                    isactive = model.Active,
                    UserId = model.UserID,
                    Check = 7,

                };

                var result = await Connection.QueryAsync<MessageEF>("Proc_CRUD_Licensee_Master", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "UnSubscribeLicense", Controller = "LicenseProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
        }

        public async Task<Result<MessageEF>> UpdatePaymentStatus(LicenseTranModel model)
        {
            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    LicenseTransactionID = model.LicenseTransactionID,
                    UserID = model.CreatedByUserID,
                    PaymentStatus = model.PaymentStatus,
                    PaymentDate = model.PaymentDate,
                    PaymentRefNo = model.PaymentRefNo,
                    Check = 6,

                };

                var result = await Connection.QueryAsync<MessageEF>("Proc_CRUD_Licensee_Master", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "SubscribeLicense", Controller = "LicenseProvider", ReturnType = "AdminPanel", UserID = model.CreatedByUserID.ToString() });
                return res;
            }
            return res;
        }
    }
}
