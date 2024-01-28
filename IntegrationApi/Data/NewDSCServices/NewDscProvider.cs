using Dapper;
using IntegrationApi.Factory;
using IntegrationApi.Repository;
using IntegrationModels;
using LoginModels;

namespace IntegrationApi.Data.NewDSCServices
{
    public class NewDscProvider : RepositoryBase, INewDscProvider
    {
        protected NewDscProvider(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<MessageEF> SaveDSCFilePath(NewDSCRequest objParams)
        {
            MessageEF messageEF = new MessageEF();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@fileName", objParams.FileName);
                param.Add("@ID", objParams.ID);
                param.Add("@UpdateIn", objParams.UpdateIn.ToUpper());
                param.Add("@UserId", objParams.UserId);
                param.Add("@MonthYear", objParams.MonthYear);
                var result = await Connection.ExecuteScalarAsync("UpdateDSCPath", param, commandType: System.Data.CommandType.StoredProcedure);
                return messageEF;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                messageEF = null;
            }
        }
    }
}
