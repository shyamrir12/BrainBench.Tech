using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationModels
{
    public class PaymentModel
    {  
        public int? UserId { get; set; }
        public string RequestType { get; set; }
        public string MerchantCode { get; set; }
        public string MerchantTxnRefNumber { get; set; }
        public string ITC { get; set; }
        public string Amount { get; set; }
        public string Currencycode { get; set; }
        public string uniqueCustomerID { get; set; }
        public string returnURL { get; set; }
        public string S2SReturnURL { get; set; }
        public string StoSreturnURL { get; set; }
        public string TPSLTXNID { get; set; }
        public string Shoppingcartdetails { get; set; }
        public string TxnDate { get; set; }
        public string Email { get; set; }
        public string mobileNo { get; set; }
        public string Bankcode { get; set; }
        public string customerName { get; set; }
        public string CardID { get; set; }
        public string AccountNo { get; set; }
        public string IsKey { get; set; }
        public string IsIv { get; set; }

        public string ErrorFile { get; set; }
        public string PropertyPath { get; set; }
        public string LogFilePath { get; set; }
        public string PaymentEncryptedData { get; set; }
        public string UserMobileNumber { get; set; }
        public string finalSent { get; set; }
        public string PaymentMode { get; set; }
        public string KOCODE { get; set; }
        public string TotalPayableAmount { get; set; }
    }
}
