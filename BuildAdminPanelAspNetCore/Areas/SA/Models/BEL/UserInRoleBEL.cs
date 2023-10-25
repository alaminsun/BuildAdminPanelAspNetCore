using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BuildAdminPanelAspNetCore.Areas.SA.Models.BEL
{
    public class UserInRoleBEL
    {
        [Required]
        //[Display(Name = "Role Name")]
        public string UserID { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string RoleID { get; set; }
        [Display(Name = "Role Name")]
        public string? RoleName { get; set; }
        public int? EmpID { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? SupervisorID { get; set; }
        public string? SupervisorName { get; set; }
        public string? EmploymentDate { get; set; }
        public string? Designation { get; set; }
        public bool IsActive { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> RoleList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> EmpList { get; set; }
        [ValidateNever]
        public List<string> SelectedItems { get; set; }


        //public string BuyerID { get; set; }

        //public string BuyerName { get; set; }
    }
}