using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SL.Sigesoft.Models
{
   public class QuotationModel
    {
        public int QuotationId { get; set; }
        public string Code { get; set; }
        public int Version { get; set; }
        public int UserCreatedId { get; set; }
        public string UserName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyRuc { get; set; }        
        public string CompanyName { get; set; }
        public string CompanyDistrictName { get; set; }
        public string CompanyAddress { get; set; }
        public int CompanyHeadquarterId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int TypeFormatId { get; set; }
        public string CommercialTerms { get; set; }
        public int StatusQuotationId { get; set; }
        public decimal? TotalQuotation { get; set; }
        public List<QuotationProfileModel> QuotationProfile { get; set; }
        public List<AdditionalComponentsQuoteModel> AdditionalComponentsQuote { get; set; }
    }

    public class QuotationProfileModel
    {
        public int QuotationProfileId { get; set; }
        public int QuotationId { get; set; }
        public string ProfileName { get; set; }
        public int? ServiceTypeId { get; set; }
        public int TypeFormatId { get; set; }
        public string ServiceTypeName { get; set; }        
        public RecordStatus RecordStatus { get; set; }        
        public RecordType RecordType { get; set; }
        public List<ProfileComponentModel> ProfileComponent { get; set; }
    }

    public class ProfileComponentModel
    {
        public int ProfileComponentId { get; set; }
        public int QuotationProfileId { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ComponentId { get; set; }
        public string ComponentName { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? PriceList { get; set; }
        public decimal? SalePrice { get; set; }

        public int? AgeConditionalId { get; set; }
        public int? Age { get; set; }
        public int? GenderConditionalId { get; set; }

        [NotMapped]
        public RecordStatus RecordStatus { get; set; }
        [NotMapped]
        public RecordType RecordType { get; set; }
    }

    public class ParamsQuotationFilterDto
    {
        public string NroQuotation { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CompanyName { get; set; }
        public int[] StatusQuotationId { get; set; }
    }

    public class AdditionalComponentsQuoteModel
    {
        public int AdditionalComponentsQuoteId { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ComponentId { get; set; }
        public string ComponentName { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? PriceList { get; set; }
        public decimal? SalePrice { get; set; }
        public int? InsertUserId { get; set; }
        [NotMapped]
        public RecordStatus RecordStatus { get; set; }
        [NotMapped]
        public RecordType RecordType { get; set; }
    }
}
