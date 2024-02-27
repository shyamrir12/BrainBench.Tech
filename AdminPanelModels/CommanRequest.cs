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
        public int? id { get; set; }
        public string? ids { get; set; }


        public string? Password { get; set; }
        public string? OldPassword { get; set; }

        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        public string? Filter { get; set; }
        
        //public string? Deptids { get; set; }
        //public string? Iids { get; set; }
        //public string? Cids { get; set; }
        //public int? Deptid { get; set; }
        //public int? Iid { get; set; }
        //public int? Cid { get; set; }
        //public int? LicenseID { get; set;
        //public int? LicenseTransactionID { get; set; }




    }
}
