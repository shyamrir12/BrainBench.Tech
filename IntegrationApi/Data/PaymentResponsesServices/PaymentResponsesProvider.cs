using Dapper;
using IntegrationModels;

using static Hangfire.Storage.JobStorageFeatures;
using System.Data;
using IntegrationApi.Repository;
using IntegrationApi.Factory;
using LoginModels;

namespace IntegrationApi.Data.PaymentResponsesServices
{
	public class PaymentResponsesProvider : RepositoryBase, IPaymentResponsesProvider
	{
        protected PaymentResponsesProvider(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<UserMasterModel> AddLicenseePaymentResponse(PaymentResponse paymentResponseDetails)
        {
            UserMasterModel userMasterModel = new UserMasterModel();
            try
            {
                var p = new DynamicParameters();
                p.Add("@ACTION", "COMPLETETRANSFERFEE");
                p.Add("@PaymentReceiptId", paymentResponseDetails.PaymentRecieptId);
                //p.Add("@LesseeID", paymentResponseDetails.UserId);
                p.Add("@ChallanNumber", paymentResponseDetails.ChallanNumber);
                p.Add("@PaymentStatus", paymentResponseDetails.PaymentStatus);
                p.Add("@PaymentAmt", paymentResponseDetails.PaidAmount);
                IDataReader dr = await Connection.ExecuteReaderAsync("USP_LICENSEE_TRANSFER", p, commandType: System.Data.CommandType.StoredProcedure);
                DataSet ds = new DataSet();
                ds = ConvertDataReaderToDataSet(dr);
                DataTable dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    userMasterModel.EMailId = Convert.ToString(dt.Rows[0]["EmailID"]);
                    userMasterModel.ApplicantName = Convert.ToString(dt.Rows[0]["ApplicantName"]);
                    userMasterModel.UserName = Convert.ToString(dt.Rows[0]["UserName"]);
                    userMasterModel.MobileNo = Convert.ToString(dt.Rows[0]["MobileNo"]);
                }
                return userMasterModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                userMasterModel = null;
            }
        }
        public async Task<MessageEF> GetPaymentResponseID(PaymentResponse paymentResponseDetails)
        {
            MessageEF messageEF = new MessageEF();
            try
            {
                var p = new DynamicParameters();
                p.Add("@SessionBank", paymentResponseDetails.SessionBank);
                p.Add("@DocContent", paymentResponseDetails.DocContent);
                p.Add("@PAYMENTFOR", 2);
                IDataReader dr = await Connection.ExecuteReaderAsync("InsertUpdatePaymentRecords", p, commandType: System.Data.CommandType.StoredProcedure);
                DataSet ds = new DataSet();
                ds = ConvertDataReaderToDataSet(dr);
                DataTable dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    messageEF.Satus = Convert.ToString(dt.Rows[0]["PaymentResponseID"]);
                }
                return messageEF;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet ConvertDataReaderToDataSet(IDataReader data)
        {
            DataSet ds = new DataSet();
            int i = 0;
            while (!data.IsClosed)
            {
                ds.Tables.Add("Table" + (i + 1));
                ds.EnforceConstraints = false;
                ds.Tables[i].Load(data);
                i++;
            }
            return ds;
        }
    }
}
