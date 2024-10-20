using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanelModels.UserMangment
{
	public class Department
    {
        //public Department()
        //{
        //    departmentList = new List<Department>();
        //}
        //public IEnumerable<ListItems> Documentissuedby { get; set; }
        //public IEnumerable<ListItems> UlbTypeList { get; set; }
        public int loginid { get; set; }
        public string? ipadress { get; set; }
        public int deptid { get; set; }

        [Required]
        [Display(Name = "Workspace Name English")]
        public string deptname { get; set; }

        [Display(Name = "Workspace Name Hindi")]
        public string deptnamehindi { get; set; }
        public bool isactive { get; set; }

        [Required]
        [Display(Name = "captcha")]
        public string captcha { get; set; }

      //  public List<Department> departmentList { get; set; }
       
        public int documentissuedval { get; set; }
       
        [Required]
        [Display(Name = "Document IssuedBy")]
        public string issuenameenglish { get; set; }
        public string ULBType { get; set; }

        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Imagethumbnail { get; set; }

      //  Deptid WarkspaceName   WarkspaceNameHindi AddedDate   isactive IsApproved  issuebyid loginid UpdatedOn UpdatedBy   Description Image   Imagethumbnail ApplicationName ApplicationNameHindi

    }
}
