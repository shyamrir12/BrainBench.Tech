using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanelModels
{
	public class RecoverPassword
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address !")]
        [Display(Name = "Email Id")]
        public string EmailId { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Input number only!")]
        [Display(Name = "Captcha")]
        public string captcha { get; set; }
    }
}
