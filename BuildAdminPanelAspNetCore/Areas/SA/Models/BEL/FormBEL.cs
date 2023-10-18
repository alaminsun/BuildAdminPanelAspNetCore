using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuildAdminPanelAspNetCore.Areas.SA.Models.BEL
{
    public class FormBEL
    {
        public string FormID { get; set; }
        [Required]
        [Display(Name = "Form Name")]
        public string FormName { get; set; }
        [Required]
        [Display(Name = "Form URL")]
        public string FormURL { get; set; }
        public string MenuImage { get; set; }
        public Boolean IsActive { get; set; }  
    }
}