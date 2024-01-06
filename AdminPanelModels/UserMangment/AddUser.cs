using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanelModels.UserMangment
{
    public class AddUser
    {

        public string roledesc { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string name { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[Remote("CheckUserExist", "AddUser", ErrorMessage = "Username already exist try another name !")]
        [RegularExpression("^[a-zA-Z]\\w+|[0-9][0-9_]*[a-zA-Z]+\\w*$", ErrorMessage = "Please follow username policy.")]
        [Display(Name = "User Name")]
        public string username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]

        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$", ErrorMessage = "Please follow password policy.minimum of 1 lower case letter,a minimum of 1 upper case letter, a minimum of 1 numeric character,a minimum of 1 special character")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string PD_Reenterpwd { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string designation { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address !")]
        [Display(Name = "Email Id")]
        public string email { get; set; }
        public int userid { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number !")]
        [Display(Name = "Mobile No")]
        public string mobile_no { get; set; }
        public string createdate { get; set; }
        public string photo { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        //  [Required(ErrorMessage = "Please choose file to upload.")]
        public string filex { get; set; }


        public string saveddocumentpath { get; set; }
        public string documentpath { get; set; }
        public int documentsize { get; set; }
        public string documentextension { get; set; }



        [Required]
        [Display(Name = "Captcha")]
        public string captcha { get; set; }
    }
}
