using AdminPanelAPI.Factory;
using AdminPanelAPI.Repository;
using AdminPanelModels;
using LoginModels;
using Dapper;


namespace AdminPanelAPI.Data.RegisterServices
{
    public class RegisterProvider : RepositoryBase, IRegisterProvider
    {
        protected RegisterProvider(IConnectionFactory connectionFactoryAuthDB) : base(connectionFactoryAuthDB)
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

        public Task<Result<List<ListItems>>> GetApplicationType()
        {
            throw new NotImplementedException();
        }

        public Task<Result<MessageEF>> RegisterUser(RegisterUser model)
        {


            throw new NotImplementedException();
        }
    }
}
