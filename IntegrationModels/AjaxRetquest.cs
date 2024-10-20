using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationModels
{
    public class AjaxRetquest
    {
        public string Type { get; set; }
        public string Data { get; set; }
        public string TempFilePath { get; set; }
        public string PDFName { get; set; }
        public string PdfFileBase64 { get; set; }
    }

    public class TestDSC
    {
        public string PDFName { get; set; }
        public string PdfFileBase64 { get; set; }
    }
}
