using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationModels
{
	public class SMSTemplate
	{
		public int RecordID { get; set; }
		public string TemplateName { get; set; }
		public string TemplateID { get; set; }
		public string InternalTemplateID { get; set; }
		public string ContentRegistered { get; set; }
		public string ContentType { get; set; }
		public string EmailTemplate { get; set; }
		public string ReferencNo { get; set; }
		public int TemplateStatus { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string Remark { get; set; }
		public DateTime CreatedOn { get; set; }
		public int CreatedBy { get; set; }
		public DateTime ModifiedOn { get; set; }
		public int ModifiedBy { get; set; }
		public bool IsSMSTested { get; set; }
		public bool IsEmailTested { get; set; }
		public int SMSTestCount { get; set; }
		public int EmailTestCount { get; set; }
	}
}
