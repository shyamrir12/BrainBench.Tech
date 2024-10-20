using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanelModels.UserMangment
{
	public class UserMappingModel
	{


        public IEnumerable<ListItems> UserList { get; set; }
        [Required(ErrorMessage = "Select User !")]
        [Display(Name = "Users")]
        public int UserId { get; set; }

        public IEnumerable<ListItems> ApplicationList { get; set; }

        [Required(ErrorMessage = "Select Application !")]
        [Display(Name = "Application")]
        public int Iids { get; set; }
        public string Applications { get; set; }

        public IEnumerable<ListItems> WorkspaceList { get; set; }

        [Display(Name = "Workspace")]
        public int[] Dids { get; set; }
        public string Workspaces { get; set; }


        public IEnumerable<ListItems> OutletList { get; set; }

        [Display(Name = "Outlet")]
        public int[] Cids { get; set; }
        public string Outlets { get; set; }




        [Required]
        [Display(Name = "Captcha")]
        public string captcha { get; set; }
    }
}
