using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
    public class ProtocolDetail
    {

        public int i_ProtocolDetailId  { get; set; }
        public int i_ProtocolId  { get; set; }
        public int i_CategoryId  { get; set; }
        public int v_CategoryName  { get; set; }
        public string v_ComponentId { get; set; }
        public string v_ComponentName { get; set; }
        public decimal? r_MinPrice { get; set; }
        public decimal? r_PriceList { get; set; }
        public decimal? r_SalePrice { get; set; }
        public int? i_AgeConditionalId { get; set; }
        public int? i_Age  { get; set; }
        public int? i_GenderConditionalId { get; set; }
        public int? i_QuotationProfileIdRef { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }
}
