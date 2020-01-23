using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SL.Sigesoft.Models
{
    public class ProfileComponent
    {
        public int i_ProfileComponentId { get; set; }
        public int i_QuotationProfileId { get; set; }
        public string v_CategoryName { get; set; }
        public int? i_CategoryId { get; set; }
        public string v_ComponentId { get; set; }
        public string v_ComponentName { get; set; }
        public decimal? r_MinPrice { get; set; }
        public decimal? r_PriceList { get; set; }
        public decimal? r_SalePrice { get; set; }
        public int? i_AgeConditionalId { get; set; }
        public int? i_Age { get; set; }
        public int? i_GenderConditionalId { get; set; }
        public YesNo i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        [NotMapped]
        public RecordStatus RecordStatus { get; set; }
        [NotMapped]
        public RecordType RecordType { get; set; }

    }
}
