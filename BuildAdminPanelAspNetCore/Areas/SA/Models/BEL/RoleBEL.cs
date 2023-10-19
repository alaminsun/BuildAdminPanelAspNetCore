using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace BuildAdminPanelAspNetCore.Areas.SA.Models.BEL
{
    public class RoleBEL 
    {
        public string RoleID { get; set; }
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        public Boolean IsActive { get; set; }            
       
       
    }
}