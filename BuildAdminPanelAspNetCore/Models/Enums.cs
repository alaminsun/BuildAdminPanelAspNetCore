using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuildAdminPanelAspNetCore.Models
{
    public class Enums
    {
        public enum E_FormFileType
        {
            CompanyLicense = 1,
            EmployeeInfo = 2,
            GMPCertification = 3,
            RecipeInfo = 4,
            ProductRegistration = 5,
            BlockList = 6,
            MeetingInfo = 7,
            NarcoticLicense = 8,
            PriceInfo = 9,
            PromotionalInfo = 10,
            AdvertisementInfo = 11,
            ImportProductRegistration = 12,
            PriceFixationCommittee = 13,
            ExportInfo = 14,
            NOCInfo = 15,
            MACertification = 16,
            BrandExport=17,
            SourceValidation=18
        };
    }
   
}