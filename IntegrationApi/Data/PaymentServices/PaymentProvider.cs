using Dapper;
using IntegrationApi.Factory;
using IntegrationApi.Repository;
using IntegrationModels;
using LoginModels;

namespace IntegrationApi.Data.PaymentServices
{
    public class PaymentProvider : RepositoryBase, IPaymentProvider
    {
        protected PaymentProvider(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<MessageEF> AddLicenseePayment(PaymentRequestModel obj)
        {
            MessageEF messageEF = new MessageEF();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Transferee_Id", obj.Licensee_Id);
                param.Add("@INT_USER_ID", obj.UserId);
                param.Add("@ACTION", "INITIATEPAYMENT");
                param.Add("@PaymentAmt", obj.TotalPayable);
                param.Add("@PaymentBank", obj.PaymentBank);
                param.Add("@PaymentMode", obj.PaymentMode);
                param.Add("@PaymentType", obj.PaymentType);
                var result = await Connection.ExecuteScalarAsync("USP_LICENSEE_TRANSFER", param, commandType: System.Data.CommandType.StoredProcedure);

                if (!string.IsNullOrEmpty(result.ToString()))
                {
                    messageEF.Satus = result.ToString();
                }
                return messageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
