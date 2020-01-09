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
        public string CommercialTerms { get; set; }
        public int? InsertUserId { get; set; }
        public List<QuotationProfileDto> QuotationProfiles { get; set; }
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
    #endregion

    #region Register

    public class QuotationRegisterDto
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
        public int? InsertUserId { get; set; }
        public List<QuotationProfileRegisterDto> QuotationProfiles { get; set; }
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

    #endregion

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
        public int? UpdateUserId { get; set; }
        public List<QuotationProfileUpdateDto> QuotationProfiles { get; set; }
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
        public string StatusName { get; set; }
        public DateTime? USDate { get; set; }
        public string TrackingDescription { get; set; }
    }
        #endregion
}
