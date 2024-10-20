using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationModels
{
	public class SMSResponseData
	{
		public string RequestSMSUrl { get; set; }
		public string SMSResponse { get; set; }
		public string Status { get; set; }
		public string StatusCode { get; set; }
		public string ResponseFromSMSSide { get; set; }
	}
}
