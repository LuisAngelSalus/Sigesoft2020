using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
   public partial class Quotation
    {
        public Quotation()
        {
            QuotationProfiles = new List<QuotationProfile>();
        }

        public int i_QuotationId { get; set; }
        public string v_Code { get; set; }
        public int i_Version { get; set; }
        public int i_UserCreatedId { get; set; }
        public int i_CompanyId { get; set; }
        public int i_CompanyHeadquarterId { get; set; }
        public string v_FullName { get; set; }
        public string v_Email { get; set; }        
        public string v_CommercialTerms { get; set; }
        public DateTime? d_ShippingDate { get; set; }
        public DateTime? d_AcceptanceDate { get; set; }
        public int? i_StatusQuotationId { get; set; }
        public decimal r_TotalQuotation { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public List<QuotationProfile> QuotationProfiles { get; set; }        
    }
}
