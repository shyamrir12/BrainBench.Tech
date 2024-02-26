using AdminPanelAPI.Data.ExceptionDataServices;
using AdminPanelAPI.Factory;
using AdminPanelAPI.Repository;
using AdminPanelModels;
using Dapper;
using LoginModels;

namespace AdminPanelAPI.Data.RecoverPasswordServices
{
    public class RecoverPasswordProvider : RepositoryBase, IRecoverPasswordProvider
    {
        private readonly IExceptionDataProvider _exceptionDataProvider;
        public RecoverPasswordProvider(IConnectionFactory connectionFactoryAuthDB, IExceptionDataProvider exceptionDataProvider) : base(connectionFactoryAuthDB)
        {
            _exceptionDataProvider = exceptionDataProvider;
        }

        public async Task<Result<MessageEF>> GetRecoverPassword(RecoverPassword model)
        {
            Result<MessageEF> res = new Result<MessageEF>();
            try
            {
                var paramList = new
                {
                    EmailId = model.EmailId,
                    Password = model.Password,
                    Check = 1


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
                _exceptionDataProvider.ErrorList(new LogEntry { ErrorMessage = ex.Message, StackTrace = ex.StackTrace, Action = "RecoverPassword", Controller = "RecoverPasswordProvider", ReturnType = "AdminPanel", UserID = model.EmailId });
                return res;
            }
            return res;
        }
    }
}
