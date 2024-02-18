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
        public int? SubRoleId { get; set; }
        public bool? Active { get; set; }
     
    }
}
