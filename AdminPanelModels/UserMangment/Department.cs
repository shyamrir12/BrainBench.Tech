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
        public Department()
        {
            departmentList = new List<Department>();
        }
        public IEnumerable<ListItems> Documentissuedby { get; set; }
        public IEnumerable<ListItems> UlbTypeList { get; set; }
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

        public List<Department> departmentList { get; set; }
        [Required]
        [Display(Name = "Document IssuedBy")]
        public int documentissuedval { get; set; }

        public string issuenameenglish { get; set; }
        public string ULBType { get; set; }
    }
}
