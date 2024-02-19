using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanelModels
{
    public class CommanRequest
    {
        public int? Check { get; set; }
        public int? UserID { get; set; }
        public int? SubRoleId { get; set; }//parent id
        public bool? Active { get; set; }
        public int? RoleId { get; set; }
        public string? MenuID { get; set; }
        public string? Deptids { get; set; }
        public string? Iids { get; set; }
        public string? Cids { get; set; }

    }
}
