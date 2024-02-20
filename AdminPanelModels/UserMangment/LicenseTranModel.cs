using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanelModels.UserMangment
{
    public class LicenseTranModel
    {
        public LicenseTranModel() {
        licenseModel=new LicenseModel ();
        }

        public LicenseModel licenseModel { get; set; }
        public int? LicenseID { get; set; }
        public int? LicenseTransactionID { get; set; }
        public string? Certificate { get; set; }
        public bool? PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentRefNo { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? CreatedByUserID { get; set; }
        public int? UpdatedByUserID { get; set; }
    }
}
