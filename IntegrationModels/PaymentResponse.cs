using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationModels
{
	public class PaymentResponse
	{
		public string SessionBank { get; set; }
		public string DocContent { get; set; }
		public string PaymentVehicleID { get; set; }
		public int? UserId { get; set; }
		public string PaymentStatus { get; set; }
		public int? UserLoginId { get; set; }
		public string TXN_STATUS { get; set; }
		public string CLNT_TXN_REF { get; set; }
		public string PaymentRecieptId { get; set; }
		public string ChallanNumber { get; set; }

		public string PaidAmount { get; set; }
	}
}
