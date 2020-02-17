using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SL.Sigesoft.Models
{
    public partial class AdditionalComponentsQuote
    {
        public int i_AdditionalComponentsQuoteId { get; set; }
        public int i_QuotationId { get; set; }
        public int i_CategoryId { get; set; }
        public string v_CategoryName { get; set; }
        public string v_ComponentId { get; set; }
        public string v_ComponentName { get; set; }
        public decimal? r_MinPrice { get; set; }
        public decimal? r_PriceList { get; set; }
        public decimal r_SalePrice { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }
        [NotMapped]
        public RecordStatus RecordStatus { get; set; }
        [NotMapped]
        public RecordType RecordType { get; set; }
        public virtual Quotation Quotation { get; set; }
    }
}
