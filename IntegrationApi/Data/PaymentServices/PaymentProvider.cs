using Dapper;
using IntegrationApi.Factory;
using IntegrationApi.Repository;
using IntegrationModels;
using LoginModels;
using System.Data;

namespace IntegrationApi.Data.PaymentServices
{
    public class PaymentProvider : RepositoryBase, IPaymentProvider
    {
        protected PaymentProvider(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<MessageEF> AddLicensePayment(PaymentRequestModel obj)
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

        public async Task<PaymentModel> GetPaymentGateway(PaymentRequestModel PaymentDetail)
        {
            PaymentModel paymentResponseModel = new PaymentModel();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Bank", PaymentDetail.PaymentBank);
                param.Add("@UserId", PaymentDetail.UserId);
                param.Add("@PrefixofMineralConcession", PaymentDetail.PrefixMineralConcession);
                //param.Add("@PaymentRefId", licensePaymentDetail.PaymentRefId);
                IDataReader dr = await Connection.ExecuteReaderAsync("getPaymentGateway", param, commandType: System.Data.CommandType.StoredProcedure);

                DataSet ds = new DataSet();
                ds = ConvertDataReaderToDataSet(dr);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (ds.Tables.Count > 1)
                    {
                        DataTable dt2 = ds.Tables[1];
                        if (dt2 != null && dt2.Rows.Count > 0)
                        {
                            PaymentDetail.TotalPayment = Convert.ToDecimal(dt2.Rows[0]["TotalPayment"].ToString());
                        }
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        paymentResponseModel.RequestType = dt.Rows[0]["RequestType"].ToString();
                        paymentResponseModel.MerchantCode = dt.Rows[0]["MerchantCode"].ToString();
                        paymentResponseModel.ErrorFile = dt.Rows[0]["ErrorFile"].ToString();
                        paymentResponseModel.PropertyPath = dt.Rows[0]["PropertyPath"].ToString();
                        paymentResponseModel.LogFilePath = dt.Rows[0]["LogFilePath"].ToString();
                        paymentResponseModel.Bankcode = dt.Rows[0]["BankCode"].ToString();
                        paymentResponseModel.ITC = dt.Rows[0]["ITC"].ToString() + ":" + Convert.ToDecimal(PaymentDetail.TotalPayment).ToString("0.00") + "";
                        paymentResponseModel.uniqueCustomerID = dt.Rows[0]["UniqueCustomerID"].ToString();
                        paymentResponseModel.MerchantTxnRefNumber = PaymentDetail.PaymentRefId;
                        paymentResponseModel.customerName = dt.Rows[0]["CustomerName"].ToString();
                        paymentResponseModel.mobileNo = dt.Rows[0]["UserMobileNumber"].ToString();
                        paymentResponseModel.Email = dt.Rows[0]["UserEmail"].ToString();
                        paymentResponseModel.TxnDate = dt.Rows[0]["TxnDate"].ToString();
                    }

                }
                return paymentResponseModel;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                paymentResponseModel = null;
            }
        }

        public async Task<MessageEF> InsertPaymentRequest(PaymentModel model)
        {
            MessageEF objMessage = new MessageEF();
            try
            {
                var p = new DynamicParameters();
                p.Add("@PAYMENTFOR", 0);
                p.Add("@UserId", model.UserId);
                p.Add("@RequestType", model.RequestType);
                p.Add("@MerchantCode", model.MerchantCode);
                p.Add("@MerchantTxnRefNumber", model.MerchantTxnRefNumber);
                p.Add("@ITC", model.ITC);
                p.Add("@Amount", model.Amount);
                p.Add("@CurrencyCode", model.Currencycode);
                p.Add("@UniqueCustomerID", model.uniqueCustomerID);
                p.Add("@ReturnUrl", model.returnURL);
                p.Add("@S2SReturnURL", model.S2SReturnURL);
                p.Add("@TPSLTXNID", model.TPSLTXNID);
                p.Add("@Shoppingcartdetails", model.Shoppingcartdetails);
                p.Add("@TxnDate", model.TxnDate);
                p.Add("@Email", model.Email);
                p.Add("@MobileNo", model.mobileNo);
                p.Add("@Bankcode", model.Bankcode);
                p.Add("@CustomerName", model.customerName);
                p.Add("@PropertyPath", model.PropertyPath);
                p.Add("@PaymentEncryptedData", model.PaymentEncryptedData);
                p.Add("@PaymentMode", "NET-BANKING");
                var result = await Connection.ExecuteScalarAsync("InsertUpdatePaymentRecords", p, commandType: CommandType.StoredProcedure);

                if (!string.IsNullOrEmpty(Convert.ToString(result)))
                {
                    objMessage.Satus = result.ToString();

                }
                else
                {
                    objMessage.Satus = "0";
                }
            }
            catch (Exception ex)
            {
                objMessage.Satus = "-1";
                throw ex;
            }
            return objMessage;
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
