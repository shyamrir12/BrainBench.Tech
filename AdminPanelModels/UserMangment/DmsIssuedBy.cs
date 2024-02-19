using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanelModels.UserMangment
{
	public class DmsIssuedBy
	{
        //public DmsIssuedBy()
        //{
        //    issuedByList = new List<DmsIssuedBy>();
        //}
        
        public int loginid { get; set; }
         public string? ipadress { get; set; }
        public int issuedid { get; set; }

        [Required]
        [Display(Name = "Application Name English")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string issuedbyname { get; set; }

        [Required]
        [Display(Name = "Application Name Hindi")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string issuedbynamehindi { get; set; }
        public bool? isactive { get; set; }

        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Imagethumbnail { get; set; }

        [Required]
        [Display(Name = "captcha")]
        public string captcha { get; set; }

      //  public List<DmsIssuedBy> issuedByList { get; set; }
    }
}
