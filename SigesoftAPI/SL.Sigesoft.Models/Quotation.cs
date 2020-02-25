using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;

namespace SL.Sigesoft.Models
{
    public partial class Quotation
    {
        public Quotation()
        {
            AdditionalComponentsQuote = new HashSet<AdditionalComponentsQuote>();
            QuotationProfile = new HashSet<QuotationProfile>();
            QuoteTracking = new HashSet<QuoteTracking>();
        }

        public int i_QuotationId { get; set; }
        public int i_ResponsibleSystemUserId { get; set; }
        public string v_Code { get; set; }
        public int i_Version { get; set; }
        public YesNo i_IsProccess { get; set; }
        public int i_CompanyId { get; set; }
        public int i_CompanyHeadquarterId { get; set; }
        public string v_FullName { get; set; }
        public string v_Email { get; set; }
        public string v_CommercialTerms { get; set; }
        public DateTime? d_ShippingDate { get; set; }
        public DateTime? d_AcceptanceDate { get; set; }
        public int i_StatusQuotationId { get; set; }
        public decimal r_TotalQuotation { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public virtual Company Company { get; set; }
        public virtual CompanyHeadquarter CompanyHeadquarter { get; set; }
        public virtual SystemUser ResponsibleSystemUser { get; set; }
        public virtual ICollection<AdditionalComponentsQuote> AdditionalComponentsQuote { get; set; }
        public virtual ICollection<QuotationProfile> QuotationProfile { get; set; }
        public virtual ICollection<QuoteTracking> QuoteTracking { get; set; }
    }
}
