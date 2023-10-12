using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuildAdminPanelAspNetCore.Models
{
    public class FileDetailModel
    {
        public long FileID { get; set; }
        public string RefNo { get; set; }
        public string FileCode { get; set; }
        public string FileName { get; set; }
        public string Extention { get; set; }
        public string FileSize { get; set; }
        public string RefLevel1 { get; set; }
        public string RefLevel2 { get; set; }
        public Nullable<int> FileType { get; set; }
        public Nullable<System.DateTime> SetOn { get; set; }
        public Nullable<int> SetBy { get; set; }
        public virtual ICollection<FileDetailModel> FileDetails { get; set; }
    }
}