using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanelModels
{
    public class MyFileRequest
    {
        public string? FileName { get; set; }
        public string? FolderName { get; set; }
        public string? FileContenctBase64 { get; set; }
        public string? FileNameWithPath { get; set; }

    }

    public class MyFileResult
    {
        public Stream Filestream { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public string FileBase64 { get; set; }
        public object FileStream1 { get; set; }
    }
}
