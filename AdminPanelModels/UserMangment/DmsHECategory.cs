using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanelModels.UserMangment
{
	public class DmsHECategory
	{
        //public DmsHECategory()
        //{
        //    hecategoryList = new List<DmsHECategory>();
        //}
        //public IEnumerable<ListItems> Documentcategory { get; set; }
        //public IEnumerable<ListItems> Documentissuedby { get; set; }
        public int loginid { get; set; }
        public string? ipadress { get; set; }
        public int hecatid { get; set; }

        [Required]
        [Display(Name = "Category Name English")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string categoryname { get; set; }

        [Required]
        [Display(Name = "Category Name Hindi")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string categorynamehindi { get; set; }
        public bool isactive { get; set; }

        [Required]
        [Display(Name = "captcha")]
        public string captcha { get; set; }
        [Required]
        [Display(Name = "Document IssuedBy")]


        public int documentissuedval { get; set; }
        [Required]
        [Display(Name = "Document Category")]
        public int documentcatval { get; set; }

        public string issuenameenglish { get; set; }
        public string documentnameenglish { get; set; }
      //  public List<DmsHECategory> hecategoryList { get; set; }

        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Imagethumbnail { get; set; }
    }
}
