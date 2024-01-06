using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationModels
{
	public class LogEntry
	{

		public string Controller { get; set; }
		public string Action { get; set; }
		public string ReturnType { get; set; }
		public string ErrorMessage { get; set; }
		public string StackTrace { get; set; }
		public string CreatedDate { get; set; }
		public string UserLoginID { get; set; }
		public string UserID { get; set; }

	}
}
