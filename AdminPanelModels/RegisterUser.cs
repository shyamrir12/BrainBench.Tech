using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanelModels
{
	public class RegisterUser
	{
		
		[EmailAddress(ErrorMessage = "Invalid Email Address !")]
		[Display(Name = "Email Id")]
        public string EmailId { get; set; }

        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number !")]
		public string Mobile_No { get; set; }
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
        public string Password { get; set; }

        [Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]

		[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$", ErrorMessage = "Please follow password policy.minimum of 1 lower case letter,a minimum of 1 upper case letter, a minimum of 1 numeric character,a minimum of 1 special character")]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		public string PD_Reenterpwd { get; set; }



        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[Remote("CheckUserExist", "AddUser", ErrorMessage = "Username already exist try another name !")]
        [RegularExpression("^[a-zA-Z]\\w+|[0-9][0-9_]*[a-zA-Z]+\\w*$", ErrorMessage = "Please follow username policy.")]
        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        [Required]
        [RegularExpression(@"^([0-9])$", ErrorMessage = "Invalid Application !")]
        public string Iid { get; set; }

        public IBrowserFile userphoto { get; set; }

        [Required]
		[RegularExpression(@"^([0-9]{6})$", ErrorMessage = "Invalid OTP !")]
		public string otp { get; set; }

       


    }
}
