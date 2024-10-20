using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanelModels.UserMangment
{
    public class LicenseType
    {
        public int? LicenseTypeID { get; set; }
        public string? LicenseTypeName { get; set; }
        public int? NumberOfUser { get; set; }
        public int? DurationInMonth { get; set; }
    }
}
