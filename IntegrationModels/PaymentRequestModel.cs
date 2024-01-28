using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationModels
{
    internal class PaymentRequestModel
    {
        public int? Transferee_Id { get; set; }
        public decimal? TotalPayable { get; set; }
        public string FinancialYear { get; set; }
        public string PaymentStatus { get; set; }
        public string ChallanNumber { get; set; }
        public string PaymentReqId { get; set; }
        public int? UserId { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentBank { get; set; }
        public string Pmode { get; set; }
        public string NetBanking_mode { get; set; }
        public string ApplicantName { get; set; }
        public string PaymentRecieptId { get; set; }
        public decimal PaymentAmt { get; set; }
        public string PaymentType { get; set; }
    }
}
