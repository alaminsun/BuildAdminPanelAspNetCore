using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace BuildAdminPanelAspNetCore.Areas.SA.Models.BEL
{
    public class SoftwareBEL
    {

        public string SoftwareID { get; set; }
        [Required]
        [Display(Name = "Software Name")]
        public string SoftwareName { get; set; }
        public Boolean IsActive { get; set; }
        [Required]
        [Display(Name = "Software ShortName")]
        public string SoftwareShortName { get; set; }
    }
}
