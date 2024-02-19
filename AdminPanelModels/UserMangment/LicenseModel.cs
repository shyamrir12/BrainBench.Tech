using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanelModels.UserMangment
{
	public class LicenseModel
	{
        public int? LicenseID { get; set; }
        public string  UserID { get; set; }
        public string? LicenseName { get; set; }
        public decimal MRate { get; set; }
        public int? discount { get; set; }
        public string? LicensePhoto { get; set; }
        public string? IPAddress { get; set; }
        public bool IsActive { get; set; }
        public int? LicenseTypeID { get; set; }
        public int? Iid { get; set; }
    }
}
