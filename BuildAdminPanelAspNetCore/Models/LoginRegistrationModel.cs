using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuildAdminPanelAspNetCore.Models
{
    public class LoginRegistrationModel
    {
        public string UserID { get; set; }
        public string Password { get; set; }
        public string RoleID { get; set; }
        public string RoleName { get; set; }

        public int EmpID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string SupervisorID { get; set; }
        public string SupervisorName { get; set; }
        public string EmploymentDate { get; set; }
        public string Designation { get; set; }
        public bool IsActive { get; set; }
    }
}