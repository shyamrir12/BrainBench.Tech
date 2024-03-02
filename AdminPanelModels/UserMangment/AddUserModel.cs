using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanelModels.UserMangment
{
    public class AddUserModel
    {
        //public IEnumerable<ListItems> Roletype { get; set; } = new List<ListItems> { new ListItems { Value = "", Text = "" } };

        [Required]
        [Display(Name = "Role")]
        public string? RoleId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string? Name { get; set; }
       

          [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string? Designation { get; set; }
       [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address !")]
        [Display(Name = "Email Id")]
        public string? EmailId { get; set; }
        public int? UserID { get; set; }
        public int? ParentID { get; set; }
               //  [Required]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number !")]
        [Display(Name = "Mobile No")]
        public string? Mobile_No { get; set; }
        public string? CreateDate { get; set; }
        public string? Photo { get; set; }

        //public IEnumerable<ListItems> ApplicatonList { get; set; } = new List<ListItems> { new ListItems { Value = "", Text = "" } };
        //[Display(Name = "Applicaton")]
        public int? Iid { get; set; }
        public string? ApplicationName { get; set; }
        //public IEnumerable<ListItems> WorkspaceList { get; set; } = new List<ListItems> { new ListItems { Value = "", Text = "" } };
        //[Display(Name = "Workspace")]
        public int? Deptid { get; set; }
        public string? WorkspaceName { get; set; }

        public string? addupdatestatus { get; set; }

        //public IEnumerable<ListItems> OutletList { get; set; } = new List<ListItems> { new ListItems { Value = "", Text = "" } };
        //[Display(Name = "Outlet")]
        public int? Cid { get; set; }
        public string? OutletName { get; set; }
       
        //[Required]
        //[RegularExpression(@"^[0-9]*$", ErrorMessage = "Input number only!")]
        //[Display(Name = "Captcha")]
        // public string? captcha { get; set; }
    }
}
