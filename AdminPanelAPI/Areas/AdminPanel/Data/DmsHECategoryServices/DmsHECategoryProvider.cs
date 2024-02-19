using AdminPanelAPI.Data.ExceptionDataServices;
using AdminPanelAPI.Factory;
using AdminPanelAPI.Repository;
using AdminPanelModels;
using AdminPanelModels.UserMangment;
using Dapper;
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

        public async Task<Result<MessageEF>> AddOutlet(DmsHECategory model)
        {
            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    categoryName=model.categoryname,
                    categoryNameHindi=model.categorynamehindi,
                    loginid = model.loginid,
                    ipaddress = model.ipadress,
                    Description = model.Description,
                    Image = model.Image,
                    Imagethumbnail = model.Imagethumbnail

                };


                var result = await Connection.QueryAsync<MessageEF>("Proc_Add_DMSHECategory", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "AddOutlet", Controller = "DmsHECategoryProvider", ReturnType = "AdminPanel", UserID = "" });
                return res;
            }
            return res;
        }

        public async Task<Result<DmsHECategory>> GetOutletBYID(CommanRequest model)
        {
            Result<DmsHECategory> res = new Result<DmsHECategory>();
            try
            {
                var paramList = new
                {
                    hecid= model.Cid,
                    UserId = model.UserID

                };

                var result = await Connection.QueryAsync<DmsHECategory>("Proc_Get_TBL_Document_HECategory_Master", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "GetOutletBYID", Controller = "DmsHECategoryProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
        }

        public async Task<Result<List<DmsHECategory>>> GetOutletList(CommanRequest model)
        {
            Result<List<DmsHECategory>> res = new Result<List<DmsHECategory>>();
            try
            {
                var paramList = new
                {

                    UserId = model.UserID

                };

                var result = await Connection.QueryAsync<DmsHECategory>("Proc_Get_TBL_Document_HECategory_Master", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "GetOutletList", Controller = "DmsHECategoryProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
        }

        public async Task<Result<MessageEF>> ModifyStatusOutlet(CommanRequest model)
        {
            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    hecid = model.Cid,
                    isactive = model.Active,
                    UserId = model.UserID

                };

                var result = await Connection.QueryAsync<MessageEF>("Proc_ModifyStatus_DMSHECategory", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "ModifyStatusOutlet", Controller = "DmsHECategoryProvider", ReturnType = "AdminPanel", UserID = model.UserID.ToString() });
                return res;
            }
            return res;
        }

        public async Task<Result<MessageEF>> UpdateOutlet(DmsHECategory model)
        {
            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    hecid=model.hecatid,
                    categoryName = model.categoryname,
                    categoryNameHindi = model.categorynamehindi,
                    loginid = model.loginid,
                    ipaddress = model.ipadress,
                    Description = model.Description,
                    Image = model.Image,
                    Imagethumbnail = model.Imagethumbnail

                };


                var result = await Connection.QueryAsync<MessageEF>("Proc_Update_DMSHECategory", paramList, commandType: System.Data.CommandType.StoredProcedure);

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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "UpdateOutlet", Controller = "DmsHECategoryProvider", ReturnType = "AdminPanel", UserID = "" });
                return res;
            }
            return res;
        }
    }
}
