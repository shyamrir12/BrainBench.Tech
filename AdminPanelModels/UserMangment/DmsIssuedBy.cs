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
        public string? sno  { get; set; }
        public int? loginid { get; set; }
         public string? ipadress { get; set; }
        public int? issuebyid { get; set; }

        [Required]
        [Display(Name = "Application Name English")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string nameenglish { get; set; }
      
       // [Required]
        [Display(Name = "Application Name Hindi")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string? namehindi  { get; set; }

        public bool? IsActive { get; set; } = false;
        public bool? IsDelete { get; set; } = false;

        public DateTime? submitdate { get; set; }
        

        public string? Description { get; set; }
        public string? ApplicationImage { get; set; }
        public string? ApplicationImagebase64 { get; set; }
        public string? Imagethumbnail { get; set; }
        public string? ImagethumbnailImagebase64 { get; set; }

        // [Required]
        [Display(Name = "captcha")]
        public string? captcha { get; set; }

      //  public List<DmsIssuedBy> issuedByList { get; set; }
    }
}
