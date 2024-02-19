using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanelModels.UserMangment
{
    public class LicenseTranModel
    {
        public int? LicenseID { get; set; }
        public int? LicenseTransactionID { get; set; }
        public string? Certificate { get; set; }
        public bool? PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentRefNo { get; set; }
    }
}
