using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanelModels
{
    public class MyFileRequest
    {
        public string FileName;
        public string FolderName;
        public string FileContenctBase64;
        public string FileNameWithPath;

    }

    public class MyFileResult
    {
        public Stream Filestream;
        public string ContentType;
        public string FileName;
        public string Status;
        public string FileBase64;
        public object FileStream1;
    }
}
