using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  IntegrationModels

{
	public class CommonMail
	{
		public string To { get; set; }
		public string? Subject { get; set; }
		public string? Salutation { get; set; }
		public string? Body { get; set; }
		public List<string> ParameterList { get; set; }
		public string TemplateID { get; set; }

	}
}
