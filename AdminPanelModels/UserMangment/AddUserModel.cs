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
        public IEnumerable<ListItems> Roletype { get; set; } = new List<ListItems> { new ListItems { Value = "", Text = "" } };


        [Display(Name = "Role")]
        public string? roleval { get; set; }

       // [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string? name { get; set; }
       

      //  [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string? designation { get; set; }
     //  [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address !")]
        [Display(Name = "Email Id")]
        public string? EmailId { get; set; }
        public int? userid { get; set; }

      //  [Required]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number !")]
        [Display(Name = "Mobile No")]
        public string? mobile_no { get; set; }
        public string? createdate { get; set; }
        public string? photo { get; set; }

        public IEnumerable<ListItems> ApplicatonList { get; set; } = new List<ListItems> { new ListItems { Value = "", Text = "" } };
        [Display(Name = "Applicaton")]
        public string? Iid { get; set; }

        public IEnumerable<ListItems> WorkspaceList { get; set; } = new List<ListItems> { new ListItems { Value = "", Text = "" } };
        [Display(Name = "Workspace")]
        public string? Did { get; set; }


        public IEnumerable<ListItems> OutletList { get; set; } = new List<ListItems> { new ListItems { Value = "", Text = "" } };
        [Display(Name = "Outlet")]
        public string? Cid { get; set; }
      //  [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Input number only!")]
        [Display(Name = "Captcha")]
         public string? captcha { get; set; }
    }
}
