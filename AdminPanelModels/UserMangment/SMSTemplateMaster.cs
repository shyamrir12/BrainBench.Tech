using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanelModels.UserMangment
{
	public class SMSTemplateMaster
	{
		public int RecordID { get; set; }
		public int? UserID { get; set; }
		public int? UserLoginID { get; set; }
		public string TemplateName { get; set; }
		public string TemplateID { get; set; }
		public string InternalTemplateID { get; set; }
		public string ContentRegistered { get; set; }
		public string ContentType { get; set; }
		public string EmailTemplate { get; set; }
		public string ReferencNo { get; set; }
		public string TemplateStatus { get; set; }
		public string IsActive { get; set; }
		public int IsDeleted { get; set; }
		public string Remark { get; set; }
		public string CreatedOn { get; set; }
		public int CreatedBy { get; set; }
		public string ModifiedOn { get; set; }
		public int ModifiedBy { get; set; }
		public int IsSMSTested { get; set; }
		public int IsEmailTested { get; set; }
		public int SMSTestCount { get; set; }
		public int EmailTestCount { get; set; }

		public int IsActiveBool { get; set; }
		public string PhoneNo { get; set; }
		public string EmailId { get; set; }
	}
}
