using BuildAdminPanelAspNetCore.Areas.SA.Models.BEL;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BuildAdminPanelAspNetCore.Areas.SA.Models.ViewModels
{
    public class UserInRoleViewModel 
    {
        public string SelectedRoleId { get; set; }
        public int SelectedEmpId { get; set; }
        public UserInRoleBEL userInRoleBEL { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> roleList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> EmpList { get; set; }
    }
}