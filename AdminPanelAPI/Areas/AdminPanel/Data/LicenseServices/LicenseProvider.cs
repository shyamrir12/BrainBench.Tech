using AdminPanelAPI.Data.ExceptionDataServices;
using AdminPanelAPI.Factory;
using AdminPanelAPI.Repository;
using AdminPanelModels;
using AdminPanelModels.UserMangment;
using Dapper;
using LoginModels;

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

        public Task<Result<LicenseModel>> GetLicenseBYID(CommanRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<LicenseModel>>> GetLicenseList(CommanRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<LicenseTranModel>>> GetLicenseTranList(CommanRequest model)
        {
            throw new NotImplementedException();
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

        public Task<Result<MessageEF>> ModifyStatusLicense(CommanRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<MessageEF>> SubscribeLicense(LicenseTranModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<MessageEF>> UnSubscribeLicense(CommanRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<MessageEF>> UpdatePaymentStatus(CommanRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
