using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Dtos
{

    #region Select
    
    public class QuotationDto
    {
        public int QuotationId { get; set; }
        public string Code { get; set; }
        public string Version { get; set; }
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
        public string CommercialTerms { get; set; }
        public int StatusQuotationId { get; set; }
        public decimal? TotalQuotation { get; set; }
        public int? InsertUserId { get; set; }
        public List<QuotationProfileDto> QuotationProfiles { get; set; }
        public List<AdditionalComponentsQuoteDto> AdditionalComponentsQuotes { get; set; }
    }

    public class QuotationProfileDto
    {
        public int QuotationProfileId { get; set; }
        public int QuotationId { get; set; }
        public string ProfileName { get; set; }
        public int? ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }
        public int? InsertUserId { get; set; }
        public RecordStatus RecordStatus { get; set; }
        public RecordType RecordType { get; set; }
        public List<ProfileComponentDto> ProfileComponents { get; set; }
    }

    public class ProfileComponentDto
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
        public int? InsertUserId { get; set; }
        public RecordStatus RecordStatus { get; set; }
        public RecordType RecordType { get; set; }
    }

    public class AdditionalComponentsQuoteDto
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
        public RecordStatus RecordStatus { get; set; }
        public RecordType RecordType { get; set; }
    }
    #endregion

    #region Register

    public class QuotationRegisterDto
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
        public string CommercialTerms { get; set; }
        public int StatusQuotationId { get; set; }
        public decimal? TotalQuotation { get; set; }
        public int? InsertUserId { get; set; }
        public List<QuotationProfileRegisterDto> QuotationProfiles { get; set; }
        public List<AdditionalComponentsQuoteRegisterDto> AdditionalComponentsQuotes { get; set; }
    }

    public class QuotationProfileRegisterDto
    {
        public int QuotationId { get; set; }
        public string ProfileName { get; set; }
        public int? ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }
        public int? InsertUserId { get; set; }
        public List<ProfileComponentRegisterDto> ProfileComponents { get; set; }
    }

    public class ProfileComponentRegisterDto
    {
        public int QuotationProfileId { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ComponentId { get; set; }
        public string ComponentName { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? PriceList { get; set; }
        public decimal? SalePrice { get; set; }
        public int? InsertUserId { get; set; }
    }

    public class AdditionalComponentsQuoteRegisterDto
    {
        public int QuotationId { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ComponentId { get; set; }
        public string ComponentName { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? PriceList { get; set; }
        public decimal? SalePrice { get; set; }
        public int? InsertUserId { get; set; }
    }

    #endregion
    public class QuotationUpdateProcess
    {
        public int QuotationId { get; set; }
        public string Code { get; set; }
    }

    #region Update
    public class QuotationUpdateDto
    {
        public int QuotationId { get; set; }
        public string Code { get; set; }
        public int Version { get; set; }
        public int CompanyId { get; set; }        
        public int CompanyHeadquarterId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string CommercialTerms { get; set; }
        public int StatusQuotationId { get; set; }
        public decimal? TotalQuotation { get; set; }
        public int? UpdateUserId { get; set; }
        public List<QuotationProfileUpdateDto> QuotationProfiles { get; set; }
        public List<AdditionalComponentsQuoteUpdateDto> AdditionalComponentsQuotes { get; set; }
    }

    public class AdditionalComponentsQuoteUpdateDto
    {
        public int QuotationId { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ComponentId { get; set; }
        public string ComponentName { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? PriceList { get; set; }
        public decimal? SalePrice { get; set; }
        public int? InsertUserId { get; set; }
        public RecordStatus RecordStatus { get; set; }
        public RecordType RecordType { get; set; }
    }
    public class QuotationProfileUpdateDto
    {
        public int QuotationProfileId { get; set; }
        public int QuotationId { get; set; }       
        public int? ServiceTypeId { get; set; }
        public string ProfileName { get; set; }
        public int? UpdateUserId { get; set; }
        public RecordStatus RecordStatus { get; set; }
        public RecordType RecordType { get; set; }
        public List<ProfileComponentUpdateDto> ProfileComponents { get; set; }
    }

    public class ProfileComponentUpdateDto
    {
        public int ProfileComponentId { get; set; }
        public int QuotationProfileId { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ComponentId { get; set; }
        public string ComponentName { get; set; }        
        public decimal? SalePrice { get; set; }
        public int? UpdateUserId { get; set; }
        public RecordStatus RecordStatus { get; set; }
        public RecordType RecordType { get; set; }
    }
    #endregion

    #region Filter
    
    public class QuotationFilterDto
    {
        public int QuotationId { get; set; }
        public string NroQuotation { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? AcceptanceDate { get; set; }
        public string CompanyName { get; set; }
        public decimal Total { get; set; }
        public DateTime? USDate { get; set; }
        public string TrackingDescription { get; set; }
        public int StatusQuotationId { get; set; }
        public string StatusQuotationName { get; set; }
        public string Indicator { get; set; }
        public List<QuoteTrackingFilterDto> QuoteTrackings { get; set; }
    }
    public class QuoteTrackingFilterDto
    {
        public int QuoteTrackingId { get; set; }
        public int QuotationId { get; set; }
        public DateTime? Date { get; set; }
        public string Commentary { get; set; }
    }
    #endregion



    #region NEW VERSION

    public class QuotationNewVersionDto
    {
        
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
        public string CommercialTerms { get; set; }
        public int StatusQuotationId { get; set; }
        public decimal? TotalQuotation { get; set; }
        public int? InsertUserId { get; set; }
        public List<QuotationProfileNewVersionDto> QuotationProfiles { get; set; }
        public List<AdditionalComponentsQuoteNewVersionDto> AdditionalComponentsQuotes { get; set; }
    }

    public class QuotationProfileNewVersionDto
    {        
        public string ProfileName { get; set; }
        public int? ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }
        public int? InsertUserId { get; set; }
        public List<ProfileComponentNewVersionDto> ProfileComponents { get; set; }
    }

    public class ProfileComponentNewVersionDto
    {        
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ComponentId { get; set; }
        public string ComponentName { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? PriceList { get; set; }
        public decimal? SalePrice { get; set; }
        public int? InsertUserId { get; set; }
    }

    public class AdditionalComponentsQuoteNewVersionDto
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ComponentId { get; set; }
        public string ComponentName { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? PriceList { get; set; }
        public decimal? SalePrice { get; set; }
        public int? InsertUserId { get; set; }
    }


    #endregion

    public class QuotationVersionDto
    {
        public int QuotationId { get; set; }
        public string NroQuotation { get; set; }
        public int Version { get; set; }
        public YesNo IsProccess { get; set; }
        public DateTime? ShippingDate { get; set; }
        public string CompanyName { get; set; }
        public decimal Total { get; set; }
        public DateTime? USDate { get; set; }
        public string TrackingDescription { get; set; }
        public int StatusQuotationId { get; set; }
        public string StatusQuotationName { get; set; }        
        
    }
}
