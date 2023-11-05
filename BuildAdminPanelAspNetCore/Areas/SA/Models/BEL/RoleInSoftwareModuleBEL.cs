using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuildAdminPanelAspNetCore.Areas.SA.Models.BEL
{
    public class RoleInSoftwareModuleBEL
    {

        public int SL { get; set; }
        public string RoleID { get; set; }
        public string SoftwareID { get; set; }
        public string SoftwareName { get; set; }
        public string ModuleID { get; set; }
        public string ModuleName { get; set; }
        public Boolean IsActive { get; set; }

        public virtual ICollection<RoleInSoftwareModuleBEL> detailsList { get; set; }
      //  public List<RoleInSoftwareModuleBEL> dtDetailsList { get; set; }
    }

    public class RoleInSoftwareMasterBEL
    {

        public string RoleID { get; set; }
        public List<RoleInSoftwareDetailBEL> DetailList { get; set; }

    }
    public class RoleInSoftwareDetailBEL
    {
        public string SoftwareID { get; set; }
        public string ModuleID { get; set; }
        [ValidateNever]
        public string ModuleName { get; set; }
        public Boolean IsActive { get; set; }
    }
}