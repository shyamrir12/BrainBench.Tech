using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationModels
{
    public class NewDSCRequest
    {
        public string FileName { get; set; }
        public string ID { get; set; }
        public string UpdateIn { get; set; }
        public string UserId { get; set; }
        public string MonthYear { get; set; }
    }
}
