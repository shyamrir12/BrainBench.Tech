using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationModels
{
    public class PaymentEF
    {
        public string BulkPermitId { get; set; }
        public string BulkPermitNo { get; set; }

        public string PaymentType { get; set; }
        public int? UserId { get; set; }
        public string SubUserId { get; set; }
        public string PaymentBank { get; set; }
        public int? PaymentMode { get; set; }
        public string PaymentModeString { get; set; }
        public string RequestType { get; set; }
        public string MerchantCode { get; set; }
        public string MerchantTxnRefNumber { get; set; }
        public string ITC { get; set; }
        //public string Amount { get; set; }
        public string AMOUNT { get; set; }
        public string Currencycode { get; set; }
        public string UniqueCustomerID { get; set; }
        public string returnURL { get; set; }
        public string StoSreturnURL { get; set; }
        public string TPSLTXNID { get; set; }
        public string Shoppingcartdetails { get; set; }
        public string TxnDate { get; set; }
        public string Email { get; set; }
        public string EMailId { get; set; }
        public string UserEmail { get; set; }

        //public string mobileNo { get; set; }
        public string MobileNo { get; set; }
        public string UserMobileNumber { get; set; }

        public string BankCode { get; set; }
        // public string BANKCODE { get; set; }

        public string CustomerName { get; set; }
        public string CardID { get; set; }
        public string AccountNo { get; set; }
        public string IsKey { get; set; }
        public string IsIv { get; set; }

        public string ErrorFile { get; set; }
        public string PropertyPath { get; set; }
        public string LogFilePath { get; set; }
        public string SBI { get; set; }
        public string ICICI { get; set; }
        public decimal? MappingValue { get; set; }
        public decimal? TotalPayableAmount { get; set; }
        public int? PAYMENTFOR { get; set; }
        public string S2SReturnURL { get; set; }
        public string PaymentEncryptedData { get; set; }
        public string CLINETTXNNO { get; set; }
        public string ICICI_TXN_NO { get; set; }
        public string UTR_Number { get; set; }
        public string ICICIRESPONSE { get; set; }
        public string metaData { get; set; }
        public int? BANK_PAY_ID { get; set; }
        public string Txn_Status { get; set; }
        public string Permit_Type { get; set; }
        public string Responce { get; set; }
        public string PG_Transaction_Date { get; set; }
        public string Challan_Initiate_Date { get; set; }
        public string Challan_Number { get; set; }
        public string Challan_Settlement_Date { get; set; }
        public string ApplicantName { get; set; }
        public string TransactionalId { get; set; }
        public string PaymentReceiptID { get; set; }
        public string forwadedDate { get; set; }
        public decimal? ApprovedQty { get; set; }
        public decimal? MineralName { get; set; }
        public decimal? MineralGrade { get; set; }
        public decimal? MineralNature { get; set; }
        public decimal? UnitName { get; set; }
        public string PaymentID { get; set; }
        public string PaymentVehicleID { get; set; }
        public int? UserLoginId { get; set; }
        public int? CHECK { get; set; }
        public string FromDATE { get; set; }
        public string ToDATE { get; set; }
        public string P_Mode { get; set; }
        public string Prefix { get; set; }
        public int PayDeptId { get; set; }
        public string OrderNo { get; set; }
        public int PaymentTypeID { get; set; }
        public int IsApplicable { get; set; }
        public int MakePayementId { get; set; }
        //public string TXNDATE { get; set; }
        public string MineralNameS { get; set; }
        public string MineralGradeS { get; set; }
        public string MineralNatureS { get; set; }
        public string UnitNameS { get; set; }

        public int OrderNStatus { get; set; }
        public string HeadAccount { get; set; }
        public string SLPayId { get; set; }
        public string WalletAdjustedAmount { get; set; }


    }
    public class PaymentResult
    {
        public List<PaymentEF> PaymentLst { get; set; }
        public List<PaymentEF> TransLst { get; set; }
        public List<PaymentEF> LesseeList { get; set; }
        public List<PaymentEF> CoalEPermitList { get; set; }
    }
    public class FinalPaymentModel
    {
        public string PaymentType { get; set; }
        public string PaymentAmount { get; set; }
    }
    public class PaymentTransaction
    {
        public string strPGRESPONSE { get; set; }
        public string TXN_STATUS { get; set; }
        public string CLNT_TXT_Type { get; set; }
        public string CLNT_TXN_REF { get; set; }
        public string TPSL_BANK_CD { get; set; }
        public string TPSL_TXN_ID { get; set; }
        public string TXN_AMT { get; set; }
        public string TPSL_TXN_TIME { get; set; }
        public string PayableRoyalty { get; set; }
        public string TCS { get; set; }
        public string Cess { get; set; }
        public string eCess { get; set; }
        public string DMF { get; set; }
        public string NMET { get; set; }
        public string MonthlyPeriodicAmount { get; set; }
        public string UserCode { get; set; }
        public string UserLogin { get; set; }
        public string PayMode { get; set; }
        public string GIBNo { get; set; }
        public int? UserId { get; set; }
        public string PaymentResponseID { get; set; }
        public string UserType { get; set; }
        public string ApplicantName { get; set; }
        public string PaymentFor { get; set; }
    }
    public class BankRemittanceModel
    {
        public long SRNO { get; set; }
        public string ID { get; set; }
        public string ClientTxnRefNumber { get; set; }
        public string MerchantRefNo { get; set; }
        public string TXN_STATUS { get; set; }
        public string TPSL_TXN_TIME { get; set; }
        public string Amount { get; set; }
    }


    

   
}
