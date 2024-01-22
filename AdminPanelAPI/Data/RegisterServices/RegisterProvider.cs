using AdminPanelAPI.Factory;
using AdminPanelAPI.Repository;
using AdminPanelModels;
using LoginModels;
using Dapper;
using System.Security.Cryptography;
using System.Reflection;


namespace AdminPanelAPI.Data.RegisterServices
{
    public class RegisterProvider : RepositoryBase, IRegisterProvider
    {
        public RegisterProvider(IConnectionFactory connectionFactoryAuthDB) : base(connectionFactoryAuthDB)
        {
        }

        public async Task<Result<MessageEF>> CheckUserExist(RegisterUser model)
        {
            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    UserName = model.username,
                    Email=model.EmailId,
                    Mobile=model.Mobile_No,
                    OrganizationName= model.username,
                    Check = 2,

                };

                var result = await Connection.QueryAsync<MessageEF>("AdminPanel_ValidateUser", paramList, commandType: System.Data.CommandType.StoredProcedure);

                if (result.Count() > 0)
                {

                    res.Data = result.FirstOrDefault();
                    res.Status = true;
                    res.Message = new List<string>() { "Successfull!" };
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
                res.Message.Add("Exception Occured! - " + ex.Message.ToString());
                return res;
            }
            return res;
        }

        public async Task<Result<List<ListItems>>> GetApplicationType()
        {
            
           Result< List < ListItems >> res = new Result<List<ListItems>> ();
            try
            {
               

                var result = await Connection.QueryAsync<ListItems>("Get_TBL_Document_IssueBy_Master", commandType: System.Data.CommandType.StoredProcedure);

                if (result.Count() > 0)
                {

                    res.Data = result.ToList();
                    res.Status = true;
                    res.Message = new List<string>() { "Successfull!" };
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
                res.Message.Add("Exception Occured! - " + ex.Message.ToString());
                return res;
            }
            return res;
        }

        public async Task<Result<MessageEF>> RegisterUser(RegisterUser model)
        {


            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    UserName = model.username,
                    Email = model.EmailId,
                    Mobile = model.Mobile_No,
                    OrganizationName = model.OrganizationName,
                    PD_Reenterpwd = model.PD_Reenterpwd,
                    Iid=model.Iid,
                    Check = 2,

                };

                var result = await Connection.QueryAsync<MessageEF>("AdminPanel_ValidateUser", paramList, commandType: System.Data.CommandType.StoredProcedure);

                if (result.Count() > 0)
                {

                    res.Data = result.FirstOrDefault();
                    res.Status = true;
                    res.Message = new List<string>() { "Successfull!" };
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
                res.Message.Add("Exception Occured! - " + ex.Message.ToString());
                return res;
            }
            return res;
        }
    }
}
