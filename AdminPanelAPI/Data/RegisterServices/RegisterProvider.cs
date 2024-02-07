using AdminPanelAPI.Factory;
using AdminPanelAPI.Repository;
using AdminPanelModels;
using LoginModels;
using Dapper;
using System.Security.Cryptography;
using System.Reflection;
using System.Text;

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

        public static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i <= bytes.Length - 1; i++)
                    builder.Append(bytes[i].ToString("x2"));

                return builder.ToString();
            }
        }
        public async Task<Result<MessageEF>> RegisterUser(RegisterUser model)
        {
             model.PD_Reenterpwd = ComputeSha256Hash(model.PD_Reenterpwd);

            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    //Name = model.username,
                    Email = model.EmailId,
                    Mobile = model.Mobile_No,
                    //OrganizationName = model.OrganizationName,
                    password = model.PD_Reenterpwd,
                   // Iid=model.Iid,
                    Check = 1,

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
                return res;
            }
            return res;
        }
    }
}
