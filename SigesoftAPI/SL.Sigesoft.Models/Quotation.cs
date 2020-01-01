using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
   public class Quotation
    {
        public int i_QuotationId { get; set; }
        public string v_Code { get; set; }
        public int i_Version { get; set; }
        public int i_UserCreated { get; set; }
        public int i_CompanyId { get; set; }
        public int i_CompanyHeadquarterId { get; set; }
        public string v_FullName { get; set; }
        public string v_Email { get; set; }
        public int i_TypeFormatId { get; set; }
        public string v_CommercialTerms { get; set; }
        public int? i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }
}
