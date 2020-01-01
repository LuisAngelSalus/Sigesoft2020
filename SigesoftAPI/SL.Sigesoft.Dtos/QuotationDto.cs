using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Dtos
{
    public class QuotationDto
    {
        public int QuotationId { get; set; }
        public string Code { get; set; }
        public int Version { get; set; }
        public int UserCreatedId { get; set; }
        public string UserName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDistrictName { get; set; }
        public string CompanyAddress { get; set; }
        public int CompanyHeadquarterId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int TypeFormatId { get; set; }
        public string CommercialTerms { get; set; }

        public List<QuotationProfileDto> QuotationProfiles { get; set; }
    }

    public class QuotationProfileDto
    {
        public int? ProfileId { get; set; }
        public string ProfileName { get; set; }
        public int? ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }

        public List<ProfileComponentDto> ProfileComponents { get; set; }
    }

    public class ProfileComponentDto
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ComponentId { get; set; }
        public string ComponentName { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? PriceList { get; set; }
        public decimal? SalePrice { get; set; }
    }
}
