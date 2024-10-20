using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationModels
{
	public class EmailSMSScheduler
	{
		public int SchedulerId { get; set; }
		public string SchedulerFor { get; set; }
		public string ParameterList { get; set; }
		public string EmailId { get; set; }
		public string MobileNo { get; set; }
		public int IsSMSSend { get; set; }
		public int IsEmailSend { get; set; }
		public String UserId { get; set; }
		public String IsActive { get; set; }
		public String CreatedBy { get; set; }
		public String CreatedOn { get; set; }
		public string TemplateID { get; set; }
	}
}
