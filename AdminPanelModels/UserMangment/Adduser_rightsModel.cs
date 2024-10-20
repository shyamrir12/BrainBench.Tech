using LoginModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanelModels.UserMangment
{
	public class Adduser_rightsModel
    {
        public Adduser_rightsModel()
        {
            Items = new List<MenuItem>();
            this.MenulistList = new List<User_right>();
        }
        public IEnumerable<ListItems> Deptuser { get; set; }
        [Required]
        [Display(Name = "User")]
        public int userval { get; set; }

        public List<MenuItem> Items;

        public List<User_right> MenulistList { get; set; }
      
        [Required]
        [Display(Name = "Captcha")]
        public string captcha { get; set; }

    }
    public class User_right
    {
        public string categoryid { get; set; }
        public bool IsSelected { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? Parentcategoryid { get; set; }
    }
}
